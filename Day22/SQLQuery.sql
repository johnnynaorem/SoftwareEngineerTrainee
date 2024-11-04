CREATE DATABASE MYCOLLECTION

USE MYCOLLECTION

CREATE TABLE AREAS(
	Id int IDENTITY(101,1) constraint pk_area primary key,
	ZipCode varchar(70),
	Address varchar(100)
)

CREATE TABLE STUDENTS(
	Id int IDENTITY(201,1) constraint pk_student primary key,
	Name varchar(100),
	Address int constraint fk_area foreign key references AREAS(Id),
)

CREATE TABLE SKILLS(
	Skill varchar(20) constraint pk_skill primary key,
	Skill_Description varchar(1000)

)

CREATE TABLE STUDENT_SKILLS(
	Student_ID int constraint fk_student_id foreign key references Students(Id),
	Student_Skill varchar(20) constraint fk_skill_skill foreign key references Skills(skill),
	Skill_level float,
	constraint pk_student_skill primary key(Student_ID,Student_Skill)
)

sp_help Students

select * from STUDENTS
select * from AREAS

drop TABLE STUDENTS;
drop TABLE STUDENT_SKILLS;

Alter table students
drop column remarks

INSERT INTO AREAS (ZipCode, Address)
VALUES ('795126', 'Ngakchourokpokpi'), ('795124', 'Yumnam Khunou'); 

INSERT INTO STUDENTS
VALUES ('Johnny',101 ); 

INSERT INTO STUDENTS (Name, Address)
VALUES ('Jaswant',101 ), ('Lanchen',102);

INSERT INTO SKILLS(Skill, Skill_Description)
VALUES ('C#','a OOPS programming language' ), ('SQL','Database'); 

INSERT INTO STUDENT_SKILLS(Student_Id, Student_Skill, Skill_level )
VALUES (201,'C#', 8 ), (202,'SQL', 7);

select * from Student_SKILLS

UPDATE student_skills
set Skill_Level = 9
where Student_Id = 201 AND Student_Skill = 'C#'

delete student_skills where student_Id=201



delete from Students where Id = 201
drop Table Students

select * from students where Address = 101
select * from AREAS where Id = 101