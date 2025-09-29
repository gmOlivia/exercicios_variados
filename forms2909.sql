create database forms2909;
use forms2909;

create table veiculo (
modelo varchar(100),
cor varchar(100),
placa varchar(100),
codigo int primary key auto_increment
);

create table vagas (
idvaga int not null,
numero int not null,
horario datetime(24) null,
stats varchar(45) null, 
primary key (idvaga)
);


