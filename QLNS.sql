CREATE DATABASE QLNHANSU

USE QLNHANSU

CREATE TABLE DEPARTMENT
(
	DEPID VARCHAR (50) primary key,
	TEN Nvarchar (50)
);


CREATE TABLE EMPLOYEE
(
	ID VARCHAR (50) primary key,
	TenNV Nvarchar (50),
	GTINH NVARCHAR (5),
	CITY NVARCHAR (50),
	DEPID VARCHAR (50),
	foreign key (DEPID) references DEPARTMENT (DEPID)
);

INSERT INTO DEPARTMENT VALUES
('1', N'Khoa CNTT'),
('2', N'Khoa ngoại ngữ'),
('3', N'Khoa khoa tài chính'),
('4', N'Phòng đào tạo');

INSERT INTO EMPLOYEE VALUES
('NV01',N'Nguyễn Hải Sơn',N'Nam',N'Đà Lạt',2),
('NV02',N'Châu Gia Long',N'Nam',N'TP HCM',3),
('NV03',N'Nguyễn Thị Minh Thư',N'Nữ',N'Quảng Ngãi',1),
('NV04',N'Nguyễn Trinh Hoàng Tú',N'Nam',N'Vũng Tàu',1),
('NV05',N'Phùng Dương Thiên Phú',N'Nam',N'TP HCM',1),
('NV06',N'Nguyễn Thị Mỹ Duyên',N'Nữ',N'Vũng Tàu',4);
select * from EMPLOYEE where DEPID = '1'


