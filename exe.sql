create database exe;
use exe;


create table usuario(
codigo int primary key auto_increment ,
nome varchar(200),
email varchar(200),
CPF varchar (100),
fone varchar(100),
ativo tinyint
);