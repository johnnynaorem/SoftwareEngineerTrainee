use Northwind

--1) Learn what is cross join
--Use Northwind database for the following queries
--2) Print the product from the category 'Dairy Products'
	select * from Products where CategoryID in 
	(select CategoryID from Categories where CategoryName = 'Dairy Products')
--3) Print the products supplied by 'Tokyo Traders'
	select * from Products where SupplierID in
	(select SupplierID from Suppliers where CompanyName = 'Tokyo Traders')

--4) Print the categories in which 'Tokyo Traders' supply products
	select * from Categories where CategoryID in(
	select CategoryID from products where SupplierID in
	(select SupplierID from Suppliers where CompanyName = 'Tokyo Traders'))

--5) Print all orders by customers from 'Spain'
	select * from Orders where CustomerID in
	(select customerID from Customers where Country = 'Spain')

--6) Print the Customer name and the freight charge
	select ContactName Customer_Name, freight Freight_Charge
	from customers c
	join orders o
	on c.CustomerID = o.CustomerID

--7) Print product name and quantity sold for all orders
	select ProductName Product_Name, Quantity Quantity_Sold
	from Products p	
	join "Order Details" o
	on p.ProductID = o.ProductID
	
--8)print the products that are billed and the unbilled products with the price and sale price
--and the difference
	select p.ProductName, p.UnitPrice Billed, od.UnitPrice Unbilled, (p.UnitPrice - od.UnitPrice) Difference
	from [Order Details] od	
	join Products p
	on od.ProductID = p.ProductID
	select * from products
--9) Print the order number, Customer name, Product name and the quantity sold for all orders
	select o.OrderID Order_Number, ContactName Customer_Name, ProductName Product_Name, Quantity Quantity_Sold
	from Orders o
	join "Order Details" od
	on o.OrderID = od.OrderID
	join  Products p
	on od.ProductID = p.ProductID
	join Customers c
	on o.CustomerID = c.CustomerID
	
--10) Print the total order amount for every order(price*quantity)+freight
	--select OrderID, Sum(Freight) TotalAmount from Orders group by OrderId
	select od.OrderID, Sum(od.UnitPrice*od.Quantity + o.Freight) TotalAmount from "Order Details" od
	join Orders o
	on od.OrderID = o.OrderId
	group by od.OrderId

--11) Print the customer name, Phone, shipper name, phone for every order
	
		select OrderID orderId, ContactName Customer_Name, c.Phone PhoneNo, s.CompanyName Shipper_Name, s.Phone PhoneNo
		from Orders o
		join Customers c
		on o.CustomerID = c.CustomerID
		join Shippers s
		on o.ShipVia = s.ShipperID
		order by Customer_Name

--12) print the shipper name and number of order by the shipper and the total freight charge

	select Shippers.CompanyName, count(OrderID) TotalOrders, Sum(Freight) TotalFreight
	from Shippers
	join Orders
	on Shippers.ShipperID = Orders.ShipVia
	group by Shippers.CompanyName

--13) Print the product name, customer name, total quantity bought for all products sold by employees from 'USA'
	
	select p.ProductName, c.ContactName,  Sum(Quantity) TotalQuantity
	from [Order Details] od
	join Products p
	on od.ProductID = p.ProductID
	join Orders o
	on od.OrderID = o.OrderID
	join Customers c
	on o.CustomerID = c.CustomerID
	join Employees e
	on o.EmployeeID = e.EmployeeID
	where e.country = 'USA'
	group by p.ProductName, c.ContactName

--14) Print the product name, category and the total sale amount sorted by category, Include all products and all categories
	
	select p.ProductName, c.CategoryName, Sum(od.Unitprice* od.Quantity) TotalSalesAmount
	from [Order Details] od
	join Products p
	on od.ProductID = p.ProductID
	join Categories c
	on p.CategoryID = c.CategoryID
	group by p.ProductName, c.CategoryName
	order by c.CategoryName


--15) Print the category name and the total sale for category for all
	
	select c.CategoryName, Count(o.OrderID) TotalSales from [Order Details] od
	join Orders o 
	on od.OrderID = o.OrderID
	join Products p
	on od.ProductID = p.ProductID
	join Categories c
	on p.CategoryID = c.CategoryID
	group by CategoryName

		
