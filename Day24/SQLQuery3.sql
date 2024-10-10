use pubs

select * from titles where pub_id = (select pub_id from publishers where pub_name = 'Binnet & Hardley')

select * from publishers

select * from Employee

select * from titles

select * from Employee where pub_id in 
(select pub_id from publishers where country = 'USA')

select * from titles where price >= (select ROUND(avg(price),2) from titles)

select round(16.454,2)

SELECT * from titles where title_id in 
(select title_id from titleauthor where au_id = 
(select au_id from authors where au_fname = 'Johnson' and au_lname = 'White'))

select * from titleauthor
select * from titles
select * from authors
select * from publishers

select au_fname, au_lname from authors where au_id in (
select au_id from titleauthor where title_id in
(select  title_id from titles where price >=15))

select * from sales

select * from titles where title_id in (

select title_id from sales where title_id in
(select title_id from titles where pub_id in
(select pub_id from publishers where pub_name = 'Binnet & Hardley')))

select Concat(SubString(title, 1,5),'...') title from titles

select * from titles

select title_id, title, pub_id,
Rank() over (order by pub_id desc) publisher_rank
from titles

select title_id, title, pub_id,
Dense_Rank() over (order by pub_id desc) publisher_rank
from titles

select pub_id,
Rank() over (order by pub_id desc) publisher_rank
from titles where pub_id = '1389'


select * from ( select title_id, title, pub_id, 
				dense_rank() over (order by pub_id) publisher_rank
				from titles
				) ranks
where publisher_rank = 2

--JOIN Inner
select publishers.pub_name, titles.title 
from publishers
join titles
on
publishers.pub_id = titles.pub_id

select * from "Order Details"
select * from Products
select * from sales
select * from stores

select titles.title, titles.title_id, sales.ord_num, sales.qty
from titles
join sales
on
titles.title_id = sales.title_id

--print the employee full name and his job_id and job description
	 
select employee.emp_id, Concat(employee.fname, ' ' ,employee.lname) name, jobs.job_id, jobs.job_desc
from employee
join jobs
on employee.job_id = jobs.job_id

--print job description and no of employees in the job
select jobs.job_desc, Count(employee.emp_id) NoOfEmployee
from jobs
join employee
on jobs.job_id = employee.job_id
group by jobs.job_desc

	--print the author name and book name
	select authors.au_fname, titles.title 
	from authors
	join titleauthor
	on titleauthor.au_id = authors.au_id
	join titles
	on titles.title_id = titleauthor.title_id
	order by title

select * from titles
select * from titleauthor
select * from publishers
select * from authors

--print author name, publisher name	and book name
select concat(authors.au_fname,' ',authors.au_lname), publishers.pub_name, titles.title
from authors
join titleauthor
on titleauthor.au_id = authors.au_id
join titles
on titles.title_id = titleauthor.title_id
join publishers
on publishers.pub_id = titles.pub_id
order by title

select concat(au_fname,' ',au_lname) 'Author name', title 'Book Name',pub_name 'Publisher Name'
from authors a join titleauthor ta
on a.au_id = ta.au_id
join titles t
on t.title_id = ta.title_id
join publishers p
on p.pub_id = t.pub_id
order by a.au_id

--Left outer join
select pub_name PublisherName, title Book_Name
from publishers p
left outer join titles t
on p.pub_id = t.pub_id


select concat(au_fname,' ',au_lname) 'Author name', title 'Book Name',pub_name 'Publisher Name'
from authors a left join titleauthor ta
on a.au_id = ta.au_id
left join titles t
on t.title_id = ta.title_id
full join publishers p
on p.pub_id = t.pub_id
order by 'Book Name' desc


--print the bill details, author name and book name of the books that are published by publisher
--who are from USA


select
ord_num Order_Number,
t.title_id,
title Book_Name,
qty Number_Of_Books_Sold,
CONCAT(au_fname,' ',au_lname) Author_Name
from sales s inner join titles t
on
s.title_id = t.title_id
join titleauthor ta
on t.title_id = ta.title_id
join authors a
on a.au_id = ta.au_id
where pub_id in (select pub_id from publishers where country = 'USA') 

--Print the author name, book name and the total quantity sold of all teh books that have 'the' 
--in the title name. Printonly of the book has sold more than 2 numbers
select
CONCAT(au_fname,' ',au_lname) Author_Name,
title Book_Name,
sum(qty) Total_Number_Of_Books_Sold

from sales s inner join titles t
on
s.title_id = t.title_id
join titleauthor ta
on t.title_id = ta.title_id
join authors a
on a.au_id = ta.au_id
where title like '%the%'
group by title,CONCAT(au_fname,' ',au_lname)
having sum(qty)>3