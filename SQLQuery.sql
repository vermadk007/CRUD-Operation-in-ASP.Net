use CRUD
go

--Registration Table---
create table Registration
(
RID int primary key identity(1,1),
Name varchar(50),
Gender int,
Qualification int,
Hobbies varchar(100),
Files varchar(100)
)

---- Stored Procedure for Insert Record ----
create proc usp_Registration_Insert
@Name varchar(50),
@Gender int,
@Qualification int,
@Hobbies varchar(100),
@Files varchar(100)
as
begin
insert into Registration(Name,Gender,Qualification,Hobbies,Files)
values(@Name,@Gender,@Qualification,@Hobbies,@Files)
end

---- Stored Procedure for Update Record----
create proc usp_Registration_Update
@RID int,
@Name varchar(50),
@Gender int,
@Qualification int,
@Hobbies varchar(100),
@Files varchar(100)
as
begin
update Registration set Name=@Name,Gender=@Gender,
Qualification=@Qualification,Hobbies=@Hobbies,Files=@Files
where RID=@RID
end

create table Gender
(
GID int primary key identity(1,1),
GName varchar(50)
)
insert into Gender(GName) values('male'),('female'),('others')



create table Qualification
(
QID int primary key identity(1,1),
QName varchar(50)
)
insert into Qualification(QName) values('mca'),('mba'),('b.tech'),('bca')

create table Hobbies
(
HID int primary key identity(1,1),
HName varchar(50)
)
insert into Hobbies(HName) values('cricket'),('music'),('movies'),('chess'),('sketching'),('carom')


create proc usp_gender_select
as
begin
select * from Gender
end

create proc usp_Qualification_select
as
begin
select * from Qualification
end

create proc usp_Hobbies_select
as
begin
select * from Hobbies
end

---- Stored Procedure for Get Record----
create proc usp_Registration_select
as
begin
select Registration.*,Gender.*,Qualification.* from Registration inner join Gender
ON Registration.Gender=Gender.GID
inner join Qualification
ON Registration.Qualification=Qualification.QID
end

---- Stored Procedure for Edit Record----
create proc usp_Registration_Edit
@RID int
as
begin
select * from Registration where RID=@RID
end

---- Stored Procedure for Delete Record----
create proc usp_Registration_Delete
@RID int
as
begin
delete from Registration where RID=@RID
end
