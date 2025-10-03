create database forms2909;
use forms2909;

create table veiculo (
modelo varchar(100),
cor varchar(100),
placa varchar(100),
codigo int primary key auto_increment
);

create table vagas (
idvaga int not null auto_increment,
numero int not null,
horario datetime(6) null,
stats varchar(45) null, 
codigo int,
primary key (idvaga), 
constraint fk_veiculo foreign key (codigo) references veiculo(codigo)
);


