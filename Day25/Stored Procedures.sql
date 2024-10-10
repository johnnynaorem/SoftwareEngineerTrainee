use MYCOLLECTION
--Stored Procedures
 
--Data Injection
select * from STUDENT_SKILLS where Student_Skill = 'C#'; delete from STUDENT_SKILLS
select * from STUDENTS
select * from SKILLS

insert into STUDENT_SKILLS values(201, 'C#', 7)
insert into STUDENT_SKILLS values(202, 'SQL', 6)

--Stored procedure is safe coz does not allow data injection
create procedure GetStudentSkills(@eskill varchar(100))
as
begin
	select * from STUDENT_SKILLS where Student_Skill = @eskill
end

drop proc GetStudentSkills

exec GetStudentSkills'C#;delete from STUDENT_SKILLS'

-----------------------
--stored procedure

--select * from EmployeesSkills
--data Injection
--select * from EmployeesSkills where Employee_id = 101;delete from EmployeesSkills
--select * from EmployeesSkills where Employee_Skill = 'C#';delete from EmployeesSkills

--insert into EmployeesSkills values(101,'C#',7),(101,'SQL',7)
--insert into EmployeesSkills values(102,'Java',8),(102,'SQL',7)

--Stored procedure is safe coz does not allow data injection
--create procedure GetEmployeeSkills(@eskill varchar(100))
--as
--begin
	--select * from EmployeesSkills where Employee_Skill = @eskill
--end

--drop proc GetEmployeeSkills

--exec GetEmployeeSkills 'C#;delete from EmployeesSkills'
