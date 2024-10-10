use pubs
--1) Print the storeid and number of orders for the store
	select s.stor_id, st.stor_name, Count(ord_num) Orders from Sales s
	left join Stores st
	on s.stor_id = st.stor_id
	group by st.stor_name, s.stor_id

--2) print the number of orders for every title
	select t.title_id, Count(ord_num) NoOfOrder from sales s
	right join titles t
	on s.title_id = t.title_id
	group by t.title_id
	

--3) print the publisher name and book name
	select p.pub_name, t.title from titles t
	left outer join publishers p
	on t.pub_id = p.pub_id

--4) Print the author full name for all the authors
	select distinct Concat(authors.au_fname, ' ', authors.au_lname) 'Full Name' from authors

--5) Print the price or every book with tax (price + price*12.36/100)
	select titles.title Book, titles.price  Price, round((titles.price+titles.price*12.36/100),2) Tax
	from titles
 

--6) Print the author name, title name
	select Concat(authors.au_fname, ' ', authors.au_lname) AuthorName, titles.title Title
	from titleauthor
	left join authors
	on titleauthor.au_id = authors.au_id
	left join titles
	on titleauthor.title_id = titles.title_id


--7) print the author name, title name and the publisher name
	select Concat(authors.au_fname, ' ', authors.au_lname) AuthorName, titles.title, publishers.pub_name from authors
	join titleauthor
	on authors.au_id = titleauthor.au_id
	join titles
	on titleauthor.title_id = titles.title_id
	join publishers
	on titles.pub_id = publishers.pub_id

--8) Print the average price of books published by every publisher
	select publishers.pub_name, AVG(titles.price) AveragePrice from titles
	right join publishers
	on titles.pub_id = publishers.pub_id
	group by publishers.pub_name

--9) print the books published by 'Marjorie'
	select titles.title, authors.au_fname from titleauthor
	join titles 
	on titleauthor.title_id = titles.title_id
	join authors
	on titleauthor.au_id = authors.au_id
	where authors.au_fname = 'Marjorie'

--10) Print the order numbers of books published by 'New Moon Books'
	select sales.ord_num, publishers.pub_name
	from sales 
	join titles
	on sales.title_id = titles.title_id
	join publishers
	on titles.pub_id = publishers.pub_id and publishers.pub_name = 'New Moon Books'
	group by publishers.pub_name, sales.ord_num

--11) Print the number of orders for every publisher
	select publishers.pub_name, Count(sales.ord_num) NoOfOrder
	from sales 
	join titles
	on sales.title_id = titles.title_id
	right join publishers
	on titles.pub_id = publishers.pub_id
	group by publishers.pub_name

--12) print the order number , book name, quantity, price and the total price for all orders
	select S.ord_num OrderNumber, 
	   T.title BookName,S.qty Quantity,T.price Price,
	   (S.qty*T.price) TotalPrice 
	from sales S
	join titles T
	on S.title_id=T.title_id

--13) print the total order quantity for every book
	select titles.title, Sum(sales.qty) totalQuantity from sales
	right join titles
	on sales.title_id = titles.title_id
	group by titles.title

--14) print the total order value for every book
	select titles.title, Count(ord_num) TotalOrder from sales
	right join titles
	on sales.title_id = titles.title_id
	group by titles.title
--15) print the orders that are for the books published by the publisher for which 'Paolo' works for
	select publishers.pub_name, Count(sales.ord_num) Orders, employee.fname EmployeeName from publishers
	join employee
	on publishers.pub_id = employee.pub_id and employee.fname = 'Paolo'
	join titles
	on publishers.pub_id = titles.pub_id
	join sales
	on titles.title_id = sales.title_id
	group by publishers.pub_name, employee.fname

