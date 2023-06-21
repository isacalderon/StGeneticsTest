 -- create database STGenetics 

create database STGenetics;

create Table animal (
    animal_id int IDENTITY(1,1) PRIMARY KEY,
    name varchar(255) not null,
    breed varchar(255) not null,
    birth_date date not null,
    sex int not null,
    price decimal(10,2) not null,
    status int not null
)

create Table sex_catalog (
    id int IDENTITY(1,1) PRIMARY KEY,
    name varchar(255) not null,
) 

create Table status_catalog (
    id int IDENTITY(1,1) PRIMARY KEY,
    name varchar(255) not null,
)