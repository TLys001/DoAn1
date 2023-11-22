CREATE TABLE tbTaiKhoan
(
	username VARCHAR(20) NOT NULL,
	password VARCHAR(20) NOT NULL,
	tentk NVARCHAR(50) NOT NULL,
	vaitro INT,
	MaNV VARCHAR(20),
	PRIMARY KEY CLUSTERED ([username] ASC),
	FOREIGN KEY (MaNV) REFERENCES tbNhanVien(MaNV)
);

INSERT INTO tbTaiKhoan (username, password, tentk, vaitro, MaNV)
VALUES ('camnghi', '1234', 'CAM NGHI', 1, 'nv03');

INSERT INTO tbTaiKhoan (username, password, tentk, vaitro)
VALUES ('admin', '0000', 'AD', 0);


select *
from tbTaiKhoan

--drop table tbTaiKhoan;

create table tbLoaiSP
(
	MaLoai varchar (20) not null ,
	TenLoai nvarchar (50) not null,
	GhiChu nvarchar (50),
	
	primary key clustered ([MaLoai] asc)
);

insert into tbLoaiSP (MaLoai, TenLoai, GhiChu)
values 
 ('La01', 'Cây lá to', ''),
 ('La02', 'Cây lá nhỏ', ''),
 ('Hoa01', 'Cây hoa đơn', ''),
 ('Hoa02', 'Cây hoa chùm', ''),
 ('Qua01', 'Cây quả to', ''),
 ('Qua02', 'Cây quả nhỏ', ''),
 ('DayLeo', 'Dây leo', ''),
 ('ThuySinh', 'Cây thủy sinh', ''),
 ('TrungSinh', 'Cây trung sinh', ''),
 ('UaHan', 'Cây ưa hạn', '');

 select *
from tbLoaiSP

create table tbNCC
(
	MaNCC varchar (20) not null ,
	TenNCC nvarchar (100) not null,
	Sdt varchar(20),
	DChi nvarchar (100),
	Email varchar(50),
	GhiChu nvarchar (50),
	primary key clustered ([MaNCC] asc)
);

insert into tbNCC (MaNCC, TenNCC, Sdt, DChi, Email, GhiChu)
values ('NCC01',N'Công ty TNHH MTV cảnh quan và cây cảnh Hà Nội','0918.396.699',N'616 Hoàng Hoa Tham, phường Bưởi, quận Tây Hồ, Hà Nội','caycanhhanoi@gmail.com',''),
('NCC02',N'Công ty cây xanh Bình Long','0971 055 578',N'346, Tổ 10, KP.Xa Cam 2, P.Hưng Chiến, TX.Bình Long, Bình Phước','vnhieu834@gmail.com',''),
('NCC03',N'Làng vườn Bách Thuận','0982888431',N'Số 10, đường 3/2, P12, Q10, TpHCM','LangVuonBachThuan.vn@gmail.com','');

--drop table tbNCC;

create table tbNhanVien
(
	MaNV varchar (20) not null ,
	TenNV nvarchar (50) not null,
	Sdt varchar(20),
	NgaySinh date,
	GioiTinh nvarchar(10),
	DChi nvarchar (50),
	NgayNhan date,
	primary key clustered ([MaNV] asc)
);
insert into tbNhanVien(MaNV, TenNV, Sdt, NgaySinh, GioiTinh, DChi, NgayNhan)
values ('NV01',N'Thiên Lý','0345006308','09/21/2002',N'Nữ',N'Bình Thủy, Cần Thơ','10/20/2023');

--drop table tbNhanVien;

create table tbKhachHang
(
	MaKH varchar (20) not null ,
	TenKH nvarchar (50) not null,
	GioiTinh nvarchar(10),
	Sdt varchar(20),
	DChi nvarchar (50),
	primary key clustered ([MaKH] asc)
);

insert into tbKhachHang(MaKH, TenKH, GioiTinh, Sdt, DChi)
values ('KH01',N'Nguyễn Văn A',N'Nữ','0123456789',N'Cái Răng, Cần Thơ');

--drop table tbKhachHang;

CREATE TABLE tbCay
(
	MaCay varchar (20) not null ,
	TenCay nvarchar (50) not null,
	MaLoai varchar (20) not null,
	MaNCC varchar (20) not null ,
	NgayNhap date,
	SoLuong int,
	GiaNhap float,
	GiaBan float,
	GhiChu nvarchar (50),
	primary key clustered ([MaCay] asc),
	FOREIGN KEY (MaLoai) REFERENCES tbLoaiSP(MaLoai),
	FOREIGN KEY (MaNCC) REFERENCES tbNCC(MaNCC)
);


insert into tbCay(MaCay, TenCay, MaLoai, MaNCC, NgayNhap, SoLuong, GiaNhap, GiaBan, GhiChu)
values ('C01',N'Hoa hồng','Hoa01','NCC03','11/11/2023', '1000', '50000', '80000', '');

CREATE TABLE tbHoaDon (
    maHD varchar (20) not null PRIMARY KEY,
    NgayBan DATE, 
    MaNV varchar (20) ,    
    MaKH varchar (20),   
    TongTien FLOAT,
    FOREIGN KEY (MaNV) REFERENCES tbNhanVien(MaNV),
    FOREIGN KEY (MaKH) REFERENCES tbKhachHang(MaKH)
);
--drop table tbHoaDon;
--insert into tbHoaDon(maHD, NgayBan, MaNV, MaKH, TongTien)
--values ('HD01','11/11/2023','NV01','KH01','80000');

CREATE TABLE tbCTHD (
    maHD varchar (20) not null ,
    MaCay varchar (20) not null,
    SoLuong INT,
    DonGia FLOAT,
	ThanhTien FLOAT,
	PRIMARY KEY (maHD, MaCay),
    FOREIGN KEY (MaCay) REFERENCES tbCay(MaCay),
	FOREIGN KEY (MaHD) REFERENCES tbHoaDon(MaHD)

);

--insert into tbCTHD(maHD, MaCay, SoLuong, DonGia, ThanhTien)
--values ('HD01','C01','1','80000','80000');

--drop table tbCTHD;

--SELECT NgayBan, SUM(TongTien) as DoanhThu 
--FROM tbHoaDon 
--GROUP BY NgayBan;

