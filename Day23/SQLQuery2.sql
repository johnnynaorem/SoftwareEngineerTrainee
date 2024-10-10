use pubs

select * from publishers where country = 'USA'

select * from publishers where country != 'USA'

select * from publishers where country <> 'USA'

select * from publishers where country = 'USA' and city = 'New York'

select * from publishers where country != 'USA' or city = 'New York'

select * from titles

select title, price, notes from titles where price > 20

select title from titles where price < 15 and price > 8

select title from titles where type in('bussiness', 'mod_cook')

select title from titles where type not in('bussiness', 'mod_cook')

select * from titles where price is null

Select * from titles where title like '%Anger%'

select * from titles where type like '_he%'

select * from employee
sp_help employee
select * from employee where fname like '%e%'
select * from employee where hire_date < '1991-10-26'
select * from employee where pub_id = 0877 and minit != ''

select * from employee order by pub_id

select * from employee order by pub_id, fname

select * from employee where fname like '%e%' order by pub_id desc

--Aggregates Functions

select Count(emp_id) total_Employee from employee
select Min(job_id) minEmployeeJobId from employee
select Max(job_id) maxEmployeeJobId from employee


select Min(price) LeastPrice from titles

select AVG(price) AveragePrice from titles where pub_id = 1389

select SUM(price) TotalPrice from titles where type = 'business'

select Max(price) HighPrice from titles

select Count(title_id) TotalBooksOfPopularComp from titles where type = 'popular_comp'

SELECT avg(price), pub_id
FROM titles
GROUP BY pub_id;

select * from titles

select * from employee

select pub_id, count(emp_id) NoOfEmployee from employee group by pub_id

select * from publishers

select country, count(pub_name) NoOfEmployee from publishers group by  country

select pub_id, count(emp_id) NoOfEmployee from employee
where fname like '%e%'
group by pub_id
having count(emp_id) > 1
order by pub_id desc

select * from employee