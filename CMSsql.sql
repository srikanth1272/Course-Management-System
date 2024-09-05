create database CourseDB

use CourseDB

create table Users(
email varchar(30) primary key,
username varchar(20) not null,
password varchar(20) not null
)
