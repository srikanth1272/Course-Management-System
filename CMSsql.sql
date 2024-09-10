create database UserDB

use UserDB

create table Users(
email varchar(30) primary key,
username varchar(20) not null,
password varchar(255) not null
)


create database SubjectDB

use SubjectDB

create table Subject(
SubjectId char(6) primary key,
Title varchar(40) unique not null,
TotalClasses int not null,
Credits int not null
)

create database StudentDB

use StudentDB

create table Student(
RollNo char(10) primary key,
FirstName varchar(20) not null,
LastName varchar(20) not null,
DOB date not null,
Email varchar(30) unique not null,
Phone char(10) unique not null,
Address varchar(100) not null
)
drop table student