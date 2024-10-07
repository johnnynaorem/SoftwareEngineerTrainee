

--Creating Employee Table
Create table Employees(
	empno int Identity(101,1) constraint pk_employee primary key,
	empname varchar(20),
	salary decimal,
	bossno int constraint fk_employee_no foreign key references Employees(empno),
)
--add Departname into Employee Table as reference
Alter table Employees 
add deptname varchar(50) constraint fk_department foreign key references Departments(deptname) null

--Inseting values into Employee table
Insert into Employees (empname, salary, bossno) 
values ('Alice ', 45000, null), 
		('Puspa ', 65000, 101),
		('John ', 75000, 101),
		('Preety ', 55000, 102) 

--Not possible for insert value in Employee Table
	Insert into Employees (empno, salary, bossno) values (1,'Alice', 45000, null)--we cannot explicitly insert value of Identity empno
	Insert into Employees (empname, salary, bossno) values ('Alice', 45000, 100) --there is no employee with employeeNumber 100 in Employee Table
	Insert into Employees (empname, salary, bossno, deptname) values
						('Ronald ', 45000, 102, 'Computer') -- there is no Department with deptname Computer in Department Table


--Updating deptname in Employee Table--
Update Employees set deptname = 'Management' where empno = 101
Update Employees set deptname = 'Clothes' where empno = 102
Update Employees set deptname = 'Equipment' where empno = 103
Update Employees set deptname = 'Navigation' where empno = 104

--Not possible for update value in Employee Table
	Update Employees set deptname = 'Computer' where empno = 101 -- there is no Department with deptname Computer in Department Table
	Update Employees set bossno = 100 where empno = 101 -- there is no employee with empno 100 in Employee Table

--Not possible for delete value in Employee Table
	Delete from Employees where empno = 101 -- Anomaly(If a tuple is deleted from referenced relation and the referenced attribute value is used by referencing attribute in referencing relation, it will not allow deleting the tuple from referenced relation.)

--Creating Department Table
Create table Departments(
	deptname varchar (50) constraint pk_department primary key,
	floor varchar(20),
	phone varchar(40),
	empno int constraint fk_employee references Employees(empno) not null,
)

--Inseting values into Department table
Insert into Departments values ('Management', '5', '34' , 101), 
								('Clothes', '2', '24', 102), 
								('Equipment', '3', '57', 103), 
								('Navigation', '1', '41', 104)


--Not possible for insert value in Department Table
	Insert into Departments values ('Management', '5', '34' , 100),  --there is no employee with employeeNumber 100 in Employee Table

--Not possible for update value in Department Table
	Update Departments set deptname = 'Books' where deptname = 'Clothes' --Anomaly(If a tuple is updated from referenced relation and the referenced attribute value is used by referencing attribute in referencing relation, it will not allow updating the tuple from referenced relation.)

--Not possible for delete value in Department table
	Delete from Departments where deptname = 'Clothes' ----Anomaly(If a tuple is deleted from referenced relation and the referenced attribute value is used by referencing attribute in referencing relation, it will not allow delating the tuple from referenced relation.)

--Not possible for Drop Tables
	Drop table Employees
	Drop table Departments

--remove constraint from employees table
ALTER TABLE Employees
DROP Constraint fk_department;

--remove constraint from Dept table
ALTER TABLE Departments
DROP Constraint fk_employee;



--Creating Sales Table

Create table Sales(
	salesno int constraint pk_sale primary key,
	saleqty int,
	itemname varchar(50) constraint fk_item foreign key references Items(itemname) not null,
	deptname varchar(50) constraint fk_department_name foreign key references Departments(deptname) not null
)

Insert into Sales values(101, 2, 'Map Measure', 'Navigation'),
						(102, 4, 'Pith Helmet', 'Equipment'),
						(103, 3, 'Elephant Polo stick', 'Clothes')

--Not possible for insert values in Sales Table
	Insert into Sales values(104, 2, 'Percy Jackson', 'Books') --there is no department with deptname Books in Department table
	Insert into Sales values(105, 4, 'Percy Jackson', 'Navigation') --there is no item with itemname Percy Jackson in Item Table

--Not possible for update values in Sales Table
	Update Sales set deptname = 'Books' where salesno = 101  --there is no deptname with deptname Books in Department table


--Creating Sales Table

Create table Items(
	itemname varchar(50) constraint pk_item primary key,
	itemtype varchar(20),
	itemcolor varchar(20),
)

--Inserting values in Items Table
Insert into Items values('Elephant Polo stick', 'R', 'Bamboo'),
						('Map Measure', 'N', 'Brown'),
						('Pith Helmet', 'C', 'Black')

--Not possible for delete values in Sales Table
	Delete from Items where itemname = 'Elephant Polo stick' ----Anomaly(If a tuple is deleted from referenced relation and the referenced attribute value is used by referencing attribute in referencing relation, it will not allow delating the tuple from referenced relation.)

--Displaying tables---
select * from Employees
--101	Alice 	45000	NULL	Management
--102	Puspa 	65000	101		Clothes
--103	John 	75000	101		Equipment
--104	Preety 	55000	102		Navigation

select * from Departments
--Clothes		2	24	102
--Equipment		3	57	103
--Management	5	34	101
--Navigation	1	41	104

select * from Sales
--101	2	Map Measure				Navigation
--102	4	Pith Helmet				Equipment
--103	3	Elephant Polo stick		Clothes

select * from Items
--Elephant Polo  stick		R	Bamboo
--Map			 Measure	N	Brown
--Pith			 Helmet		C	Black
