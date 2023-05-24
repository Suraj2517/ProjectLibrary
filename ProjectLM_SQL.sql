Create Database ProjectLM

Use ProjectLM

Create Table Books 
(
BookId int identity primary key, 
Title varchar(20),
Author varchar(20),
Publication varchar(20),
Stock int,
)

-----------------------------------------------------

Create Table Students 
(
StudentId int identity primary key, 
RollNo varchar(20),
Name varchar(20),
)

--------------------------------------------

Create Table IssuedBooks
(
BookId int references Books(BookId),
StudentId int references Students(StudentId),
IssueDate Datetime,
)

------------------------------------------------

Create Table LoginId
(
Username varchar(50),
Password varchar(50),
)

Insert into LoginId values ('Suraj167', 'Seven@167' )

--------------------------------------------------------

Drop Table Books
Drop Table Students
Drop Table IssuedBooks
Drop Table LoginId

Select * from Books
Select * from Students
Select * from IssuedBooks
Select * from LoginId