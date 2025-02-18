use nguyen5;
create table knihovna(
id int not null primary key identity(1,1),
nazev varchar(20) not null,
mesto varchar(20) not null
)
create table zamestnanec(
id int not null primary key identity(1,1),
jmeno varchar(50) not null,
prijmeni varchar(50) not null,
datum_narozeni date not null check(datum_narozeni < '2007-02-14')
)
create table kniha(
id int not null primary key identity(1,1),
nazev varchar(50) not null,
zanr varchar(50) not null,
autor varchar(50) not null,
zapujceno bit not null
)
create table evidence_zamestnancu(
id int not null primary key identity(1,1),
knihovna_id int not null foreign key references knihovna(id),
zamestnanec_id int not null foreign key references zamestnanec(id),
plat decimal(10,2) not null,
datum date not null
)
create table evidence_knihy(
id int not null primary key identity(1,1),
knihovna_id int not null foreign key references knihovna(id),
kniha_id int not null foreign key references kniha(id),
datum date not null
)

