using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SimpleShop
{
    internal class Service : IService
    {
        SqlConnection conn = new SqlConnection("Server=5CD413DKRZ\\JOHNNY_INSTANCES;Integrated Security=True; Initial Catalog=Northwind;TrustServerCertificate=True");

        
        public void CreateUser()
        {
            string username, password;
            Console.Write("Enter username: ");
            username = Console.ReadLine();
            Console.Write("Enter password: ");
            password = Console.ReadLine();
            string insertQuery = $"Insert into Users values('{username}', '{password}')";
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = insertQuery;
            try
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("User created successfully");
                }
                else
                {
                    Console.WriteLine("User creation failed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void OrderSummary()
        {
            Console.Write("Enter the Order No.: ");
            string orderID = Console.ReadLine();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand($"select * from Products where ProductId in (select ProductID from [Order Details] where OrderId in (select OrderID from Orders where OrderID = @orderID))", conn);
            adapter.SelectCommand.Parameters.AddWithValue("@OrderId", orderID);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet);
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Console.WriteLine($"Product Name: \t{row["ProductName"]}");
                    Console.WriteLine($"Supplied By: \t{row["SupplierID"]}");
                    Console.WriteLine($"Price: \t\t{row["UnitPrice"]}");
                    Console.WriteLine("-------------------------");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        void OrderDeatails()
        {
            int choice;
            do
            {
                Console.WriteLine("1. Order Summary");
                Console.WriteLine("2. View Shipper Details");
                Console.WriteLine("9. Back");

                Console.Write("Enter your choice: ");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        OrderSummary();
                        break;
                    case 2:
                        ViewShipperDetail();
                        break;
                    case 9:
                        return;
                    default:
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                        Console.ResetColor();
                        break;
                }
            } while (choice != 0);
        }
        public void PreviousOrder(string customerID)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand($"Select * from Orders where CustomerID='{customerID}'", conn);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet);
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Console.WriteLine("-------------------------");
                    Console.WriteLine($"OrderId: \t{row["OrderID"]}");
                    Console.WriteLine($"CustomerID: \t{row["CustomerID"]}");
                    Console.WriteLine("-------------------------");
                }
                OrderDeatails();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        bool CheckUser(string username, string password)
        {
            SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM Users WHERE Username=@un AND Password=@pass", conn);
            try
            {
                conn.Open();
                sqlCommand.Parameters.AddWithValue("@un", username);
                sqlCommand.Parameters.AddWithValue("@pass", password);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }
        

        public void UpdatePassword(string username)
        {
            string password, newPassword;
            Console.Write("Pleae enter your current password: ");
            password = Console.ReadLine();
            try
            {
                if (CheckUser(username, password))
                {
                    Console.Write("Please enter your new password: ");
                    newPassword = Console.ReadLine();
                    SqlCommand sqlCommand = new SqlCommand("UPDATE Users SET Password=@newpass WHERE Username=@un", conn);
                    sqlCommand.Parameters.AddWithValue("@newpass", newPassword);
                    sqlCommand.Parameters.AddWithValue("@un", username);
                    try
                    {
                        conn.Open();
                        int rowsAffected = sqlCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Password updated successfully");
                        }
                        else
                        {
                            Console.WriteLine("Password update failed");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Could not verify user. Sorry cannot update password now.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        bool GetCustomer(string customerID)
        {
            bool isCustomerExist = false;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand($"Select * from Customers where CustomerID='{customerID}'", conn);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    isCustomerExist = true;
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        Console.WriteLine($"CutomerId: \t{row["CustomerID"]}");
                        Console.WriteLine($"CustomerName: \t{row["ContactName"]}");
                        Console.WriteLine("-------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("Enter Valid CustomerID and try agian....");
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return isCustomerExist;
        }


        void MainInteraction()
        {
            Console.Write("Enter the Customer ID: ");
            string customerID = Console.ReadLine();
            if (GetCustomer(customerID))
            {
                Option(customerID);
            };  
        }

        void Option(string customerID)
        {
            int choice;
            do
            {
                Console.WriteLine("1. View Previous Orders");
                Console.WriteLine("0. Back");

                Console.Write("Enter your choice: ");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        PreviousOrder(customerID);
                        break;
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Exiting...");
                        Console.ResetColor();
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                        Console.ResetColor();
                        break;
                }
            } while (choice != 0);

        }

        void UserInteractionAfterLogin(string username)
        {

            Console.WriteLine("Login Successfull");
            int choice;
            try
            {
                do
                {
                    Console.WriteLine("1. Enter Customer ID for futher process");
                    Console.WriteLine("2. Update Password");
                    Console.WriteLine("9. Logout");

                    Console.Write("Enter your choice: ");

                    choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            MainInteraction();
                            break;
                        case 2:
                            UpdatePassword(username);
                            break;
                        case 9:
                            return;
                        default:
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                            Console.ResetColor();
                            break;
                    }
                } while (choice != 0);
            }
           catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void UserLogin()
        {
            string username, password;
            Console.Write("Please enter the username: ");
            username = Console.ReadLine();
            Console.Write("Pleae enter your password: ");
            password = Console.ReadLine();
            try
            {
                if (CheckUser(username, password))
                {
                    UserInteractionAfterLogin(username);
                }
                else
                {
                    Console.WriteLine("Could not verify user. Sorry unable to login User.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void ViewShipperDetail()
        {
            Console.Write("Enter the Order No.: ");
            string orderID = Console.ReadLine();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand($"SELECT * FROM Shippers WHERE ShipperID = (SELECT ShipVia FROM Orders WHERE OrderId = @OrderId)", conn);
            adapter.SelectCommand.Parameters.AddWithValue("@OrderId", orderID);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet);
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Console.WriteLine($"ShipperID: \t\t{row["shipperID"]}");
                    Console.WriteLine($"Shipper Company: \t{row["CompanyName"]}");
                    Console.WriteLine($"Contact: \t\t{row["Phone"]}");
                    Console.WriteLine("-------------------------");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
