CREATE DATABASE NHAKHOA
GO

USE NHAKHOA
GO

CREATE TABLE PHONG(
	MaPhong NVARCHAR(15) PRIMARY KEY
);

CREATE TABLE NHANVIEN(
	MaNhanVien INT IDENTITY(1,1),
	HoVaTen NVARCHAR(255),
	GioiTinh NVARCHAR(5),
	NgaySinh DATE,
	TienLuong INT,
	NgayBatDauLamViec DATE,
	SoNha INT,
	Phuong NVARCHAR(255),
	ThanhPho NVARCHAR(255),
	ViTriLamViec NVARCHAR(255),
	Anh IMAGE,
	SoDienThoai NVARCHAR(50),
	TenDuong NVARCHAR(255),
	PRIMARY KEY (MaNhanVien)
);

CREATE TABLE BACSI(
	HocVi NVARCHAR(255),
	ChuyenMon NVARCHAR(255),
	MaNhanVien INT,
	FOREIGN KEY (MaNhanVien) REFERENCES NHANVIEN(MaNhanVien)
);

CREATE TABLE BAOVE(
	MaNhanVien INT,
	FOREIGN KEY (MaNhanVien) REFERENCES NHANVIEN(MaNhanVien)
);

CREATE TABLE BENHNHAN(
	MaBenhNhan INT IDENTITY(1,1),
	HoVaTen NVARCHAR(255),
	GioiTinh NVARCHAR(5),
	NgaySinh DATE,
	SoNha INT,
	Phuong NVARCHAR(255),
	TenDuong NVARCHAR(255),
	ThanhPho NVARCHAR(255),
	Anh IMAGE,
	SoDienThoai NVARCHAR(50) UNIQUE,
	PRIMARY KEY (MaBenhNhan)
);

CREATE TABLE LOAIDICHVU(
	MaLoaiDichVu INT IDENTITY(1,1),
	TenLoaiDichVu NVARCHAR(MAX),
	PRIMARY KEY (MaLoaiDichVu)
);

CREATE TABLE DICHVU(
	MaDichVu INT IDENTITY(1,1),
	MaLoaiDichVu INT REFERENCES LOAIDICHVU(MaLoaiDichVu),
	TenDichVu NVARCHAR(255),
	GiaDichVu INT,
	GiamGia INT,
	DonVi INT,
	BaoHanh INT,
	ThoiGianThucHien INT,
	ChiTiet NVARCHAR(MAX),
	PRIMARY KEY (MaDichVu)
);



CREATE TABLE HOADON(
	MaHoaDon INT IDENTITY(1,1) PRIMARY KEY,
	MaBenhNhan INT REFERENCES BENHNHAN(MaBenhNhan),
	NgayThamKham DATE
);

CREATE TABLE THONGTINBENHAN(
	MaBenhAn INT IDENTITY(1,1),
	MaBenhNhan INT,
	MaNhanVien INT, 
	TrieuChung NVARCHAR(MAX),
	ChanDoan NVARCHAR(MAX),
	DanhGia NVARCHAR(MAX),
	MoTa NVARCHAR(MAX),
	PRIMARY KEY (MaBenhAn),
	FOREIGN KEY (MaBenhNhan) REFERENCES BENHNHAN(MaBenhNhan),
	FOREIGN KEY (MaNhanVien) REFERENCES NHANVIEN(MaNhanVien)
);
--Ch? c� b�c s? m?i c� quy?n th�m b?nh �n

CREATE TABLE THONGTINDICHVU(
	MaThongTin INT IDENTITY(1,1),
	MaNhanVien INT REFERENCES NHANVIEN(MaNhanVien),
	NgaySuDung DATETIME,
	SoLuong INT,
	ThoiGianThucHien INT,
	MaDichVu INT REFERENCES DICHVU(MaDichVu),
	MaHoaDon INT REFERENCES HOADON(MaHoaDon),
	PRIMARY KEY(MaThongTin)
);

CREATE TABLE THUOC(
	MaThuoc NVARCHAR(50),
	TenThuoc NVARCHAR(255),
	HuongDanSD NVARCHAR(MAX),
	ThanhPhan NVARCHAR(MAX),
	GiaNhap INT,
	GiaBan INT,
	SoLuong INT,
	CongTy NVARCHAR(50),
	PRIMARY KEY (MaThuoc)
);

CREATE TABLE DONTHUOC(
	MaDonThuoc INT IDENTITY(1,1) PRIMARY KEY,
	MaHoaDon INT REFERENCES HOADON(MaHoaDon),
	SoLuong INT
);

CREATE TABLE THONGTINSUDUNGTHUOC(
	MaThongTin INT IDENTITY(1,1),
	MaThuoc NVARCHAR(50) REFERENCES THUOC(MaThuoc),
	SoLuong INT,
	NgaySuDung DATETIME,
	MaDonThuoc INT REFERENCES DONTHUOC(MaDonThuoc),
);

/*CREATE TABLE LICHHEN(
	--MaLichHen INT IDENTITY(1,1),
--	MaNhanVien INT,
	--TenKhachHang NVARCHAR(255),
	--SoDienThoaiKhachHang NVARCHAR(50),
	--MaPhong NVARCHAR(15),
	--BatDau DATETIME,
	--ThoiGian INT,
	--NoiDung NVARCHAR(MAX),
	--PRIMARY KEY (MaLichHen),
	--FOREIGN KEY (MaNhanVien) REFERENCES NHANVIEN(MaNhanVien),
	--FOREIGN KEY (MaPhong) REFERENCES PHONG(MaPhong)
--);


CREATE TABLE LICHHEN(
	MaLichHen INT IDENTITY(1,1),
	MaNhanVien INT,
	TenKhachHang NVARCHAR(255),
	SoDienThoaiKhachHang NVARCHAR(50),
	DiaChi NVARCHAR(255),
	BatDau DATETIME,
	ThoiGian INT,
	NoiDung NVARCHAR(MAX),
	PRIMARY KEY (MaLichHen),
	FOREIGN KEY (MaNhanVien) REFERENCES NHANVIEN(MaNhanVien)
);

CREATE TABLE DANHGIA(
	MaDanhGia INT IDENTITY(1,1) PRIMARY KEY,
	MaNhanVien INT REFERENCES NHANVIEN(MaNhanVien),
	DiemDanhGia INT,
	PhanHoi NVARCHAR(255),
	NgayDanhGia DATETIME,
);