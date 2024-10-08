﻿create database UserDB

use UserDB

create table Users(
UserId int Identity primary key, 
email varchar(30) Unique not null,
username varchar(20) not null,
password varchar(255) not null,
Role varchar(10) 
)


create database CMDB

use CMDB

create table Subject(
SubjectId char(6) primary key,
Title varchar(40) unique not null,
TotalClasses int not null,
Credits int not null
)

create table Student(
RollNo char(10) primary key,
FirstName varchar(20) not null,
LastName varchar(20) not null,
DOB date not null,
Email varchar(30) unique not null,
Phone char(10) unique not null,
Address varchar(100) not null
)


create table StdSubject(
RollNo char(10) References Student(RollNo),
SubjectId char(6) References Subject(SubjectId),
Semister INT not null,
primary key(RollNo,SubjectId)
)

alter procedure stdSubjectProcedure as
Begin
 select std.rollNo ,std.SubjectId,
        CONCAT(s.RollNo,' : ',s.FirstName,' ',s.LastName) as studentdetails ,
        CONCAT(sb.SubjectId,' : ',sb.Title) as subjectdetails,
        std.Semister from StdSubject std 
        inner join Student s on std.RollNo = s.RollNo 
        inner join Subject sb on std.SubjectId = sb.SubjectId
END;

exec stdSubjectProcedure