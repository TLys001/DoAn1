create table tbTaiKhoan
(
	username varchar (20) not null ,
	password varchar (20) not null,
	tentk nvarchar (50) not null,
	vaitro tinyint,
	primary key clustered ([username] asc)
);



