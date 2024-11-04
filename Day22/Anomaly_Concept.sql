CREATE TABLE parent_table_p (
           id INT constraint pk_parent primary key,
		   name varchar(20)
)

insert into parent_table_p values(1, 'Johnny'), (2 ,'Tom')

update parent_table_p set id = 2 where id=20
delete from parent_table_p where id=1

CREATE TABLE child_table_c(
           id INT PRIMARY KEY,
           parent_id INT constraint fk_parent_id foreign key references parent_table_p(id) ON DELETE CASCADE ON UPDATE CASCADE

)

	drop table child_table_c
	drop table parent_table_p

insert into child_table_c values(101, 2), (102,1)

select * from parent_table_p
select * from child_table_c