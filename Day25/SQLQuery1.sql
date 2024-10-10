use Northwind

select * from Orders cross join Products
select * from Products

select * from Products where CategoryID =3
union 
select * from Products where SupplierID in(2,4,7)


select * from Products where CategoryID =3
union all
select * from Products where SupplierID in(2,4,7)


select * from Products where CategoryID =3
intersect
select * from Products where SupplierID in(2,4,7)

select * from Products where CategoryID =3
Except
select * from Products where SupplierID in(2,4,7)

select top 5 * from products order by UnitPrice desc

select distinct supplierID from products

--print the supplier ids who have not made any supplies
select * from suppliers where SupplierID not in (select distinct supplierID from products)

--or

select supplierID from suppliers
except
select distinct supplierID from products

--or

select * from Suppliers s
join Products p
on s.SupplierID = p.SupplierID and p.SupplierID is null


-- print those product details that have never been ordered
select * from products where productId not in (select distinct ProductID from [Order Details])

--or

select productId from Products
except 
select distinct ProductID from [Order Details]

--print the custome details who have never made any purchase
select CustomerID from Customers
Except
Select distinct customerID from orders 



select * from Employees


select c.CompanyName, c.CustomerID, o.CustomerID, o.OrderID  from Customers c
left join Orders o
on c.CustomerID = o.CustomerID 
where o.CustomerID is null



select CustomerID from Customers
except
select distinct CustomerID from Orders



--print the employee name and reports to ID
select emp.employeeID, Concat(emp.Firstname, ' ', emp.Lastname) EmployeeName, emp.ReportsTo managerID,
mgr.EmployeeID, Concat(mgr.Firstname, ' ', mgr.Lastname) Manager_Name
from Employees emp
left join Employees mgr
on emp.ReportsTo = mgr.EmployeeID

--print the products whose price is greater than product supplied by supplied Germany
select * from products

select * from Products
where UnitPrice > all(select UnitPrice from Products where SupplierID in
(select SupplierID from Suppliers where Country = 'Germany'))

select * from Products pp
join Suppliers ss
on pp.SupplierID = ss.SupplierID
where pp.UnitPrice >(
select MAX(p.UnitPrice) from Products p
join Suppliers s 
on p.SupplierID = s.SupplierID and s.Country = 'Germany')


