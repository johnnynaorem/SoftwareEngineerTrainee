select * from Employees
select * from Events
select * from Bookings
select * from Employees
join Events
on Employees.EmployeeId = Events.EmployeeId
join Bookings
on Employees.EmployeeId = Bookings.EmployeeId

SELECT DISTINCT Employees.Name, Events.Title, Bookings.BookingId, Bookings.BookingTime
FROM Employees
JOIN Events ON Employees.EmployeeId = Events.EmployeeId
JOIN Bookings ON Employees.EmployeeId = Bookings.EmployeeId;

SELECT DISTINCT Employees.Name, Events.Title, Bookings.BookingTime
FROM Employees
JOIN Events ON Employees.EmployeeId = Events.EmployeeId
JOIN Bookings ON Employees.EmployeeId = Bookings.EmployeeId;


