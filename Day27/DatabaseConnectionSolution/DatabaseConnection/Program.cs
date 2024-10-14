using Microsoft.Data.SqlClient;
using System.Data;
namespace DatabaseConnection
{
    internal class Program
    {
        SqlConnection conn = new SqlConnection("Server=5CD413DKRZ\\JOHNNY_INSTANCES;Integrated Security=True; Initial Catalog=Northwind;TrustServerCertificate=True");

        public Program() { 
            
        }

        public void GetProductFromDB()
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM PRODUCTS";

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("-------------------------");
                    Console.WriteLine($"Name: \t{reader["ProductName"]}");
                    Console.WriteLine($"Price: \t{reader["UnitPrice"]}");
                    Console.WriteLine("-------------------------");
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally {
                conn.Close();
            }
        }
        void CreateUser()
        {
            string username, password;
            Console.WriteLine("Enter username: ");
            username = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            password = Console.ReadLine();
            //string insertQuery = "Insert into Users values('"+username+"', '"+password+"')";
            string insertQuery = $"Insert into Users values('{username}', '{password}')";
            //SqlCommand cmd = new SqlCommand(insertQuery, conn);
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
                    Console.WriteLine($"Name: \t{reader["username"]}");
                    Console.WriteLine("-------------------------");
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
        void UpdatePassword()
        {
            string username, password, newPassword;
            Console.WriteLine("Please enter the username");
            username = Console.ReadLine();
            Console.WriteLine("Pleae enter your current password");
            password = Console.ReadLine();
            try
            {
                if (CheckUser(username, password))
                {
                    Console.WriteLine("Please enter your new password");
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

        void UnderstandingDisconnectedArchitecture()
        {
            Console.WriteLine($"The current connected state is {conn.State}");
            SqlDataAdapter adapter = new SqlDataAdapter();
            Console.WriteLine($"The current connected state is {conn.State}");
            adapter.SelectCommand = new SqlCommand("Select * from products", conn);
            Console.WriteLine($"The current connected state is {conn.State}");
            DataSet dataSet = new DataSet();
            Console.WriteLine($"The current connected state is {conn.State}");
            try
            {
                adapter.Fill(dataSet);
                Console.WriteLine($"The current connected state is {conn.State}");
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Console.WriteLine("-------------------------");
                    Console.WriteLine($"Name: \t{row["ProductName"]}");
                    Console.WriteLine($"Price: \t{row["UnitPrice"]}");
                    Console.WriteLine("-------------------------");
                }
            }
            catch (Exception e) { 
                Console.WriteLine(e.Message);
            }
        }

        void Dis()
        {
            using (SqlConnection conn = new SqlConnection("Server=5CD413DKRZ\\JOHNNY_INSTANCES;Integrated Security=True; Initial Catalog=Northwind;TrustServerCertificate=True"))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand("SELECT * FROM categories", conn);

                DataSet dataSet = new DataSet();

                Console.WriteLine("Connection State before Fill: " + conn.State);

                adapter.Fill(dataSet);

                Console.WriteLine("Connection State after Fill: " + conn.State);
            }
        }

        //Category
        void UnderstandingDisconnectedArchitectureForCategory()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("Select * from categories", conn);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet);
                Console.WriteLine($"The current connected state is {conn.State}");
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Console.WriteLine("-------------------------");
                    Console.WriteLine($"ID: \t{row["categoryID"]}");
                    Console.WriteLine($"Name: \t{row["categoryName"]}");
                    Console.WriteLine($"Discription: \t{row["Description"]}");
                    Console.WriteLine("-------------------------");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        void DeleteUser()
        {
            string username, password;
            Console.Write("Please enter the username: ");
            username = Console.ReadLine();
            Console.Write("Pleae enter your current password: ");
            password = Console.ReadLine();
            try
            {
                if (CheckUser(username, password))
                {
                    Console.Write("Are you want to delete this user?: ");
                    string confirm = Console.ReadLine();
                    if(confirm == "yes")
                    {
                        SqlCommand sqlCommand = new SqlCommand("Delete from Users WHERE Username=@un AND Password =@pass", conn);
                        sqlCommand.Parameters.AddWithValue("@pass", password);
                        sqlCommand.Parameters.AddWithValue("@un", username);
                        try
                        {
                            conn.Open();
                            int rowsAffected = sqlCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Deleted user successfully");
                            }
                            else
                            {
                                Console.WriteLine("Delete user failed");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Could not verify user. Sorry cannot delete user now.");
                    }
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

        static void Main(string[] args)
        {
            Program program = new Program();
            //program.CreateUser();
            //program.UpdatePassword();
            //program.UnderstandingDisconnectedArchitecture();
            //program.UnderstandingDisconnectedArchitectureForCategory();
            //program.Dis();
            program.DeleteUser();

        }
    }
}
