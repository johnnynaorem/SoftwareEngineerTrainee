use Northwind
go

-- Select All the  product details
select * from dbo.Products

--Select the product which are priced more than 10
select * from dbo.Products where UnitPrice > 10

--Print the products in the ascending order of price
select * from dbo.Products order by UnitPrice

--Print the products that are price between 10 and 25
select * from dbo.Products where UnitPrice between 10 and 25

--Print all the products that are packaged in box
select * from dbo.Products where QuantityPerUnit like '%box%'

--Print the products that are available more than 10 units and are restocked
select * from dbo.Products where UnitsInStock > 10 and ReorderLevel <> 0

--Print the name and price of all the products that are reordered
select ProductName, UnitPrice from Products where ReorderLevel <> 0
	
--Print all the customers who have no region
select * from dbo.Customers where Region is null

-- print the full name of customers
select ContactName from Customers 

--Print the number of customers from every country
select Country, Count(customerID) NoOfCustomer from dbo.Customers group by Country

--Count the number of city in every country from which we have customers
select Count(distinct City) NoOfCity, Country  from dbo.Customers group by Country

--Print the total number of sale done by every employee
select * from Orders
select EmployeeID, Count(OrderId) NoOfSales from dbo.Orders group by EmployeeID

--Print the total freight charge paid by every customer
select EmployeeId, Sum(freight) TotalFreightCharge from dbo.Orders group by EmployeeId

-- Print the total number of times every product was ordered
select ProductID, Count(OrderID) NoOfOrder from "Order Details" group by ProductID

--Print the average price of the products in descending order for every category. 
--Consider the category only if we have more than 2 products in it
select CategoryID, avg(UnitPrice) AveragePrice, 
Count(ProductID) NoOfProduct
from Products 
group by CategoryID 
having (Count(ProductID)) > 2
order by AveragePrice desc		
select * from Products

