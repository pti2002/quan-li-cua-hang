CREATE DATABASE QL_CUAHANGTIENLOI

ON PRIMARY
(
	NAME= DB_QL_CUAHANGTIENLOI_PRIMARY,
	FILENAME='C:\db_QL_CUAHANGTIENLOI_primary.mdf',
	SIZE=3MB,
	MAXSIZE=10MB,
	FILEGROWTH=10%
),
FILEGROUP DB_GROUP
(
	NAME= DB_QL_CUAHANGTIENLOI_SECOND1_1,
	FILENAME='C:\DB_QL_CUAHANGTIENLOI_second1_1.ndf',
	SIZE=3MB,
	MAXSIZE=5MB,
	FILEGROWTH=10%
)
LOG ON
(
	NAME= DB_QL_CUAHANGTIENLOI_Log,
	FILENAME='D:\DB_QL_CUAHANGTIENLOI_Log.ldf',
	SIZE=1MB,
	MAXSIZE=5MB,
	FILEGROWTH=15%
);

CREATE TABLE LOAIHANG(
	MALOAIHANG NVARCHAR(10) NOT NULL,
	TENLOAIHANG NVARCHAR(40) NOT NULL,
	CONSTRAINT PK_LOAIHANG PRIMARY KEY(MALOAIHANG)
)

CREATE TABLE KHACH(
	MAKHACH NVARCHAR(10) NOT NULL,
	TENKHACH NVARCHAR(40) NOT NULL,
	DIACHI_KH NVARCHAR(50) NOT NULL,
	DIENTHOAI_KH NVARCHAR(20) NOT NULL,
	CONSTRAINT PK_KHACH PRIMARY KEY(MAKHACH)
)
CREATE TABLE HANG(
	MAHANG NVARCHAR(10) NOT NULL,
	TENHANG NVARCHAR(50) NOT NULL,
	SOLUONG INT NOT NULL,
	MALOAIHANG NVARCHAR(10) NOT NULL,
	DONGIANHAP FLOAT NOT NULL,
	DONGIABAN FLOAT NOT NULL,
	CONSTRAINT PK_HANG PRIMARY KEY(MAHANG)	
)
CREATE TABLE NHANVIEN(
	MANHANVIEN NVARCHAR(10) NOT NULL,
	TENNHANVIEN NVARCHAR(50) NOT NULL,
	GIOITINH NVARCHAR(5) NOT NULL,
	DIACHI_NV NVARCHAR(50) NOT NULL,
	DIENTHOAI_NV NVARCHAR(20) NOT NULL,
	NGAYSINH DATETIME,
	CONSTRAINT PK_NHANVIEN PRIMARY KEY(MANHANVIEN)
)
CREATE TABLE HDBAN(
	MAHDBAN NVARCHAR(30) NOT NULL,
	MANHANVIEN NVARCHAR(10) NOT NULL,
	NGAYBAN DATETIME NOT NULL,
	MAKHACH NVARCHAR(10) NOT NULL,
	TONGTIEN FLOAT NOT NULL,

	CONSTRAINT PK_HDBAN PRIMARY KEY(MAHDBAN)
)
CREATE TABLE CHITIETHDBAN(
	MAHDBAN NVARCHAR(30) NOT NULL,
	MAHANG NVARCHAR(10) NOT NULL,
	SOLUONGMUA INT NOT NULL,
	DONGIA FLOAT NOT NULL,
	GIAMGIA FLOAT NOT NULL,
	THANHTIEN FLOAT, 
	CONSTRAINT PK_CHITIETHDBAN PRIMARY KEY(MAHDBAN,MAHANG)
)

ALTER TABLE HANG
ADD CONSTRAINT FK_HANG_LOAIHANG FOREIGN KEY(MALOAIHANG) REFERENCES LOAIHANG(MALOAIHANG)

ALTER TABLE CHITIETHDBAN
ADD CONSTRAINT FK_CHITIETHDBAN_HANG FOREIGN KEY(MAHANG) REFERENCES HANG(MAHANG)

ALTER TABLE CHITIETHDBAN
ADD CONSTRAINT FK_CHITIETHDBAN_HDBAN FOREIGN KEY(MAHDBAN) REFERENCES HDBAN(MAHDBAN)

ALTER TABLE HDBAN
ADD CONSTRAINT FK_HDBAN_NHANVIEN FOREIGN KEY(MANHANVIEN) REFERENCES NHANVIEN(MANHANVIEN)

ALTER TABLE HDBAN
ADD CONSTRAINT FK_HDBAN_KHACH FOREIGN KEY(MAKHACH) REFERENCES KHACH(MAKHACH)

-------------------------------------DỮ LIỆU------------------------------------
INSERT INTO NHANVIEN
VALUES ('NV001',N'Trần Quyết Chiến',N'Nam','Q12,TPHCM','092817212721','2/3/2003'),
	   ('NV002',N'Lê Thị Mười Điểm',N'Nữ','Q11,TPHCM','09288328392','9/3/2002'),
	   ('NV003',N'Trần Hân Hoan',N'Nam',N'Hốc Môn,TPHCM','09289291212','2/8/2001'),
	   ('NV004',N'Võ Thị Bé Ba',N'Nữ',N'Tân Bình,TPHCM','097837882392','12/2/2004')
INSERT INTO KHACH
VALUES ('KH001',N'Nguyễn Vip Tông',N'Cần Thơ','092817212721'),
	   ('KH002',N'Nguyễn Hỷ Nam',N'Củ Chi','092883281222'),
	   ('KH003',N'Trần Nhân Hồng',N'Q1,TPHCM','09282391212'),
	   ('KH004',N'Lê Na',N'Tân Bình','0987873882392'),
	   ('KH005',N'Dương Nhân Từ',N'Tân Bình','09721313392'),
	   ('KH006',N'Trần Lê Bá Cháy',N'Tân Phú','09721823392')
INSERT INTO LOAIHANG
VALUES ('LH001',N'Bánh kẹo'),
	   ('LH002',N'Nước giải khát'),
	   ('LH003',N'Sản phẩm ăn liền'),
	   ('LH004',N'Thức ăn đóng hộp'),
	   ('LH005',N'Đồ dùng gia đình'),
	   ('LH006',N'Bia,Rượu')
INSERT INTO HANG
VALUES ('HH001',N'Bánh Cosy',500,'LH001',15000,18000),
	   ('HH002',N'Kitkat',500,'LH001',17000,20000),
	   ('HH003',N'Cocacola',500,'LH002',7000,10000),
	   ('HH004',N'Mirinda vị biệt quất',1000,'LH002',8000,10000),
	   ('HH005',N'Nước khoáng Lavie',1000,'LH002',3000,5000),
	   ('HH006',N'Mì hảo hảo',1000,'LH003',3000,6000),
	   ('HH007',N'Don ăn liền',1000,'LH003',10000,14000),
	   ('HH008',N'Rong biển ăn liền',2000,'LH003',15000,20000),
	   ('HH009',N'Cá hộp 3 cô gái',2000,'LH004',20000,23000),
	   ('HH0010',N'Vải ngâm RICH lon 565g',2000,'LH004',30000,35000),
	   ('HH0011',N'Thịt hộp SPAM Mỹ',2000,'LH004',80000,90000),
	   ('HH0012',N'Bộ 10 đôi đũa tre',1000,'LH005',120000,127000),
	   ('HH0013',N'Bàn chải đánh răng',500,'LH005',15000,17000),
	   ('HH0014',N'Ly 350ML',500,'LH005',50000,55000),
	   ('HH0015',N'Thùng 24 lon bia Tiger 330ML',500,'LH006',350000,370000),
	   ('HH0016',N'Thùng 20 lon bia Budweiser 330ML',500,'LH006',300000,325000),
	   ('HH0017',N'Rượu Soju',700,'LH006',50000,55000),
	   ('HH0018',N'Rượu Vodka',800,'LH006',300000,325000)
  
INSERT INTO HDBAN
VALUES ('HDB9122022_144726','NV001','6/10/2022','KH001',0)
INSERT INTO HDBAN
VALUES ('HDB9122022_154726','NV002','6/10/2022','KH002',0)
INSERT INTO HDBAN
VALUES ('HDB10122022_154726','NV002','6/10/2022','KH003',0)

INSERT INTO CHITIETHDBAN
VALUES ('HDB9122022_144726','HH001',100,18000,0,NULL)
INSERT INTO CHITIETHDBAN
VALUES ('HDB9122022_144726','HH002',200,20000,0,NULL)
INSERT INTO CHITIETHDBAN
VALUES ('HDB9122022_144726','HH003',100,10000,0,NULL)

INSERT INTO CHITIETHDBAN
VALUES ('HDB9122022_144726','HH001',100,18000,0,360000)
INSERT INTO CHITIETHDBAN
VALUES ('HDB9122022_144726','HH003',200,10000,0,200000)

select * from KHACH
select * from LOAIHANG
select * from HANG
select * from NHANVIEN
select * from HDBAN
select * from CHITIETHDBAN
------------------------------------------------------RÀNG BUỘC------------------------------------------------------
--KT tính duy nhất của tên loại hàng
alter table LOAIHANG
add constraint UNI_TENLOAIHANG UNIQUE (TENLOAIHANG)
--KT tính duy nhất của Số điện thoại Khách Hàng
alter table KHACH
add constraint UNI_SDTKHACH UNIQUE (DIENTHOAI_KH)
--KT đơn giá nhập phải lón hơn đơn giá bán
ALTER TABLE HANG
ADD CONSTRAINT CHK_DONGIA CHECK (DONGIANHAP>0 AND DONGIABAN>0);


-----------------------------------------PROCEDURE----------------------------------------
-----------------------------------------NAM+SANG-----------------------------------------
--Xuất thông tin nhân viên đã bán đơn hàng @mahd
create proc thongtinNV @mahd char(10)
as
	select *
	from NhanVien
	where MANHANVIEN =(select MANHANVIEN
	from HDBAN
	where MAHDBAN = 'HDB9122022_154726')
go

exec thongtinNV 'HDB9122022_154726'

--Tính tổng tiền nhập mặt hàng @mahang
create proc TienNhapHang @mahang char(30)
as
	select SOLUONG*DONGIANHAP as N'Tổng tiền nhập'
	from HANG
	where MAHANG=@mahang
go

exec TienNhapHang 'HH001'

--Tính tổng doang thu 1 ngày của cửa hàng
create proc DoanhThu1Ngay @ngayban date
as
	select SUM(TONGTIEN)
	from HDBAN
	where NGAYBAN = @ngayban
go

exec DoanhThu1Ngay '6/10/2022'	

--Xuất số lượng hàng đã nhập của loại hàng @loaihang
create proc SLHang @malh char(30)
as
	select COUNT(MAHANG) as N'Tổng số hàng đã nhập'
	from LOAIHANG, HANG
	where LOAIHANG.MALOAIHANG = HANG.MALOAIHANG and HANG.MALOAIHANG = 'LH001'
go

exec TinhTongTien 'LH001'	

-----------------------------------------THỌ-----------------------------------------
--Xuất danh sách tên khách hàng mua hàng trong 1 ngày
create proc TenCacKH1Ngay @ngayban date
as
	SELECT K.MAKHACH, TENKHACH
	FROM KHACH K INNER JOIN HDBAN HD
	ON K.MAKHACH = HD.MAKHACH
	WHERE NGAYBAN = @ngayban
go
exec TenCacKH1Ngay '6/10/2022' 
--Xuất các tên sản phẩm chưa bán được trong 1 ngày
create proc TenSP @ngayban date
as
	SELECT H.MAHANG, TENHANG
	FROM HANG H
	WHERE H.MAHANG NOT IN(SELECT C.MAHANG 
	FROM CHITIETHDBAN C INNER JOIN HDBAN H
	ON C.MAHDBAN = H.MAHDBAN
	WHERE NGAYBAN = @ngayban)
go
exec TenSP '6/10/2022'

-----------------------------------------LUÂN-----------------------------------------
--Tính tuổi nhân viên
create proc TinhTuoiNV @manv char(10)
as
	select year(getdate()) - year(NGAYSINH) as N'Tuổi NV'
	from NHANVIEN where MANHANVIEN=@manv
go

exec TinhTuoiNV 'NV002'

--Tính thành tiền cho chi tiết hóa đơn
create proc TinhThanhTien @mahdban char(30),@mahang char(30)
as
	update CHITIETHDBAN
	set THANHTIEN=(select SOLUONGMUA*DONGIA as N'Thành tiền'
	from CHITIETHDBAN
	where MAHDBAN=@mahdban and CHITIETHDBAN.MAHANG=@mahang)
	where MAHDBAN=@mahdban and CHITIETHDBAN.MAHANG=@mahang
go
drop proc TinhThanhTien

exec TinhThanhTien 'HDB9122022_144726','HH001'	
select * from CHITIETHDBAN

-----------------------------------------THIỆN-----------------------------------------
--Trả về số lượng nhân viên
create proc SLNhanVien @sl int output
as
	set @sl = (select count(MANHANVIEN) from NHANVIEN)
go

--Xuất danh sách hàng có giá bán bằng giá bán nhập
create proc DSHangTheoGiaBan @giaban float
as
	select * from HANG where DONGIABAN = @giaban
go


-----------------------------------------FUNCTION----------------------------------------
-----------------------------------------NAM+SANG----------------------------------------
--Xuất thông tin nhân viên đã bán đơn hàng @mahd
create function thongtinNV (@mahd nvarchar(30))
returns table
as
	return (select *
	from NhanVien
	where MANHANVIEN =(select MANHANVIEN from HDBAN where MAHDBAN = @mahd)
	)
go

select * from dbo.thongtinNV ('HDB9122022_154726')

--Tính tổng tiền nhập mặt hàng @mahang
create function TienNhapHang (@mahang char(30))
returns int
as
	begin
	declare @tongtien int
	set @tongtien = (select SOLUONG*DONGIANHAP as N'Tổng tiền nhập'
	from HANG
	where MAHANG=@mahang)
	return @tongtien
	end
go

declare @TT int
set @TT = dbo.TienNhapHang('HH001')
print @TT

--Tính tổng doang thu 1 ngày của cửa hàng
create function DoanhThu1Ngay (@ngayban date)
returns int
as
	begin
	declare @doanhthu int
	set @doanhthu = (select SUM(TONGTIEN)
	from HDBAN
	where NGAYBAN = @ngayban)
	return @doanhthu
	end
go

declare @DT int
set @DT = dbo.DoanhThu1Ngay('6/10/2022')
print @DT

--Xuất số lượng hàng đã nhập của loại hàng @loaihang
create function SLHang (@malh char(30))
returns int
as
	begin
	declare @sl int
	set @sl = (select COUNT(MAHANG) as N'Tổng số hàng đã nhập'
	from LOAIHANG, HANG
	where LOAIHANG.MALOAIHANG = HANG.MALOAIHANG and HANG.MALOAIHANG = @malh)
	return @sl
	end
go

declare @SLH int
set @SLH = dbo.SLHang('LH001')
print @SLH

-----------------------------------THIỆN-----------------------------------
--Trả về số tiền giảm của hóa đơn khi truyền mã hóa đơn
create function tinhTienGiam(@maHD nvarchar(30))
returns float
as
	begin
		declare @tiengiam float
		set @tiengiam = (select sum(THANHTIEN*GIAMGIA) from CHITIETHDBAN where MAHDBAN = @maHD)
		return @tiengiam
	end
go

--Trả về tên, số điện thoại của khách khi truyền vào tên khách hàng
create function SDTTen(@tenKH nvarchar(40))
returns table
as
	return select TENKHACH, DIENTHOAI_KH from KHACH where TENKHACH = @tenKH
go

-----------------------------------LUÂN-----------------------------------
--Tính tuổi nhân viên
create function TinhTuoiNV_Func (@manv char(10))
returns table
as
return(select year(getdate()) - year(NGAYSINH) as N'Tuổi NV'
	from NHANVIEN where MANHANVIEN=@manv)
go

--GỌI
select * from TinhTuoiNV_Func('NV001')
GO

--Tính thành tiền
create function TinhThanhTien_Func(@mahdban char(30),@mahang char(30))
returns decimal(10,2)
as
	begin
		declare @thanhtien decimal(10,2)
			set @thanhtien=(select SOLUONGMUA*DONGIA as N'Thành tiền'
			from CHITIETHDBAN
			where MAHDBAN=@mahdban and CHITIETHDBAN.MAHANG=@mahang)
		return @thanhtien
end
go

drop function TinhThanhTien_Func

declare @thanhtien decimal(10,2)
set @thanhtien= dbo.TinhThanhTien_Func('HDB9122022_144726','HH001')
print @thanhtien 

select * from CHITIETHDBAN


-----------------------------------------CURSOR----------------------------------------
-----------------------------------------NAM-----------------------------------------
------Những NV lớn hơn 18 tuổi--------
declare cs_tuoiNV cursor
dynamic 
for 
select TENNHANVIEN, YEAR(GETDATE()) - YEAR(NGAYSINH) from NHANVIEN
open cs_tuoiNV
declare @tennv nvarchar(30), @tuoi int

fetch next from cs_tuoiNV into @tennv, @tuoi
while @@FETCH_STATUS = 0
begin 
	If (@tuoi >18) 
	begin
		print @tennv
		print @tuoi
	end
	fetch next from cs_tuoiNV into @tennv, @tuoi
end
close cs_tuoiNV
deallocate cs_tuoiNV

------Liệt kê những mặt hàng có giá trị--------
declare cs_hangGT cursor
dynamic 
for 
select TENHANG, SOLUONG * DONGIANHAP from HANG
open cs_hangGT
declare @tenhang nvarchar(30), @giatri int

fetch next from cs_hangGT into @tenhang, @giatri
while @@FETCH_STATUS = 0
begin 
	If (@giatri >100000000) 
	begin
		print @tenhang
		print @giatri
	end
	fetch next from cs_hangGT into @tenhang, @giatri
end
close cs_hangGT
deallocate cs_hangGT
-----------------------------------------SANG-----------------------------------------
-------Tình trạng mặt hàng
declare cs_TThang cursor
dynamic 
for 
select TENHANG, SOLUONG from HANG
open cs_TThang
declare @tenhang nvarchar(30), @soluong int

fetch next from cs_TThang into @tenhang, @soluong
while @@FETCH_STATUS = 0
begin 
	If (@soluong = 0) 
	begin
		print @tenhang
		print 'Hết hàng'
	end
	else
	begin
		print @tenhang
		print 'Còn hàng'
	end
	fetch next from cs_TThang into @tenhang, @soluong
end
close cs_TThang
deallocate cs_TThang
----Hiển thị tên nhân viên bán hd đó----
declare cs_NVHoaDon cursor
dynamic 
for 
select	MAHDBAN, TENNHANVIEN from HDBAN, NHANVIEN where HDBAN.MANHANVIEN = NHANVIEN.MANHANVIEN
open cs_NVHoaDon
declare @maHD nvarchar(30), @tenNV nvarchar(50)

fetch next from cs_NVHoaDon into @maHD, @tenNV
while @@FETCH_STATUS = 0
begin 
		select @maHD + ' - ' + @tenNV

	fetch next from cs_NVHoaDon into @maHD, @tenNV
end
close cs_NVHoaDon
deallocate cs_NVHoaDon

-----------------------------------THIỆN-----------------------------------
--Kiểm tra hàng sắp hết
DECLARE cs_HetHang CURSOR FOR
SELECT SOLUONG, MAHANG FROM HANG
declare @mahang nvarchar(10), @sl int
Open cs_HetHang
FETCH NEXT FROM cs_GHetHang INTO @sl, @mahang
WHILE @@FETCH_STATUS = 0
begin
	if(@sl <= 50)
		begin
			print @mahang
			print @sl
		end
	FETCH NEXT FROM cs_GHetHang INTO @sl, @mahang
end
close cs_HetHang
deallocate cs_HetHang
go

--xuất ra hàng không giảm giá
DECLARE cs_HangKhongGiam CURSOR FOR
SELECT MAHANG, GIAMGIA FROM CHITIETHDBAN
declare @mh nvarchar(10), @giamgia float
Open cs_HetHang
FETCH NEXT FROM cs_HangKhongGiam INTO @mh, @giamgia
WHILE @@FETCH_STATUS = 0
begin
	if(@giamgia = 0)
		begin
			print @mh
		end
	FETCH NEXT FROM cs_HangKhongGiam INTO @sl, @mahang
end
close cs_HangKhongGiam
deallocate cs_HangKhongGiam

-----------------------------------------LUÂN-----------------------------------------
--Tính thành tiền
declare TinhThanhTien_Cur cursor
For
	select MAHDBAN,MAHANG,SOLUONGMUA,DONGIA,THANHTIEN from CHITIETHDBAN
open TinhThanhTien_Cur
declare @mahdban char(30),@mahang char(30),@slmua int,@dg decimal(10,2),@tt decimal(10,2)
fetch next from  TinhThanhTien_Cur into @mahdban,@mahang,@slmua,@dg,@tt
while(@@FETCH_STATUS=0)
begin
	update CHITIETHDBAN
		set THANHTIEN = @slmua*@dg
	where MAHDBAN=@mahdban and MAHANG=@mahang
	fetch next from TinhThanhTien_Cur into @mahdban,@mahang,@slmua,@dg,@tt
end
close TinhThanhTien_Cur
deallocate TinhThanhTien_Cur

INSERT INTO HDBAN
VALUES ('HDB9122022_144726','NV001','6/10/2022','KH001',0)

INSERT INTO CHITIETHDBAN
VALUES ('HDB9122022_144726','HH001',100,18000,0,NULL)
INSERT INTO CHITIETHDBAN
VALUES ('HDB9122022_144726','HH002',200,20000,0,NULL)
INSERT INTO CHITIETHDBAN
VALUES ('HDB9122022_144726','HH003',100,10000,0,NULL)

select * from CHITIETHDBAN

--Tính doanh thu bán hàng trong năm 2022
declare TinhDoanhThu_Cur cursor
For
	select MAHDBAN,MANHANVIEN,NGAYBAN,MAKHACH,TONGTIEN from HDBAN
open TinhDoanhThu_Cur
declare @mahd char(30),@manv char(30),@ngayban date,@makh char(30),@tongtien decimal(10,2)
fetch next from  TinhDoanhThu_Cur into @mahd,@manv,@ngayban,@makh,@tongtien
while(@@FETCH_STATUS=0)
begin
	SELECT sum(hd.TONGTIEN) as N'Doanh thu'
	FROM HDBAN hd
	WHERE YEAR(hd.NGAYBAN) = 2022
	fetch next from TinhDoanhThu_Cur into @mahd,@manv,@ngayban,@makh,@tongtien
end
close TinhDoanhThu_Cur
deallocate TinhDoanhThu_Cur

INSERT INTO HDBAN
VALUES ('HDB9122022_144726','NV001','09/12/2022','KH001',180000)
INSERT INTO HDBAN
VALUES ('HDB10122022_144726','NV001','10/12/2022','KH001',300000)
select * from HDBAN

-----------------------------------------TRIGGER-----------------------------------------
-----------------------------------------SANG-----------------------------------------
CREATE TRIGGER KT_SOLUONG ON HANG
FOR INSERT,UPDATE
AS
	IF(SELECT SOLUONG FROM INSERTED)>0
		COMMIT TRAN
ELSE
    BEGIN
        PRINT N'SỐ LƯỢNG PHẢI LỚN HƠN 0'
        ROLLBACK TRAN
    END
--Kiem tra don gia nhap phai lon hon 0
CREATE TRIGGER KT_DONGIANHAP ON HANG
FOR INSERT,UPDATE
AS
	IF(SELECT DONGIANHAP FROM INSERTED)>0
		COMMIT TRAN
ELSE
    BEGIN
        PRINT N'ĐƠN GIÁ NHẬP PHẢI LỚN HƠN 0'
        ROLLBACK TRAN
    END
-----------------------------------------NAM-----------------------------------------
--Kiểm tra đơn giá bán phải lớn hơn đơn giá nhập
CREATE TRIGGER KT_DONGIABAN ON HANG
FOR INSERT,UPDATE
AS
	IF(SELECT DONGIABAN FROM INSERTED) > (SELECT DONGIANHAP FROM INSERTED)
		COMMIT TRAN
ELSE
    BEGIN
        PRINT N'ĐƠN GIÁ BÁN PHẢI LỚN HƠN ĐƠN GIÁ NHẬP'
        ROLLBACK TRAN
    END
--Kiểm tra giảm giá không được quá 100
CREATE TRIGGER KT_GIAMGIA ON CHITIETHDBAN
FOR INSERT,UPDATE
AS
	IF(SELECT GIAMGIA FROM INSERTED)<100
    COMMIT TRAN
ELSE
    BEGIN
        PRINT N'GIẢM GIÁ KHÔNG QUÁ 100%'
        ROLLBACK TRAN
    END

UPDATE CHITIETHDBAN
SET GIAMGIA=101
WHERE MAHDBAN='HDB9122022_144726' and MAHANG ='HH001'


-----------------------------------THIỆN-----------------------------------
--Kiểm tra giảm giá không được quá 100
--Kiểm tra xem số lượng hàng trong kho còn đủ để cung cấp không?
CREATE TRIGGER KT_SOLUONGHANGCUNGCAP ON CHITIETHDBAN
FOR INSERT,UPDATE
AS
	IF(SELECT SOLUONGMUA FROM INSERTED)>(SELECT SOLUONG FROM HANG WHERE MAHANG=(SELECT MAHANG FROM inserted))
	BEGIN 
		PRINT N'MẶT HÀNG NÀY HIỆN KHÔNG ĐỦ ĐỂ CUNG CẤP'
		ROLLBACK TRAN
	END
ELSE
	COMMIT TRAN

UPDATE CHITIETHDBAN
SET SOLUONGMUA=1000000
WHERE MAHDBAN='HDB1062022_144726'

--Cập nhật số lượng hàng sau khi thêm
CREATE TRIGGER UPDATE_THEMHD_TRIGGER ON CHITIETHDBAN AFTER INSERT 
AS	
	BEGIN
		UPDATE HANG
		SET SOLUONG=SOLUONG-(SELECT SOLUONGMUA FROM inserted WHERE MAHANG=HANG.MAHANG)
		FROM HANG
		JOIN inserted ON HANG.MAHANG=inserted.MAHANG
	END

INSERT INTO HDBAN
VALUES ('HDB9122022_144726','NV001','6/10/2022','KH001',360000)

INSERT INTO CHITIETHDBAN
VALUES ('HDB9122022_144726','HH001',10,18000,0,360000)

SELECT * FROM HANG WHERE MAHANG='HH001'
SELECT * FROM CHITIETHDBAN	WHERE MAHANG='HH001' and MAHDBAN='HDB9122022_144726'
-----------------------------------LUÂN-----------------------------------
--Kiểm tra số lượng mua phải lớn hơn 0
CREATE TRIGGER KT_SOLUONGMUA ON CHITIETHDBAN
FOR INSERT,UPDATE
AS
	IF(SELECT SOLUONGMUA FROM INSERTED)>0
    COMMIT TRAN
ELSE
    BEGIN
        PRINT N'SỐ LƯỢNG MUA PHẢI LỚN HƠN 0'
        ROLLBACK TRAN
    END

UPDATE CHITIETHDBAN
SET SOLUONGMUA=0
WHERE MAHDBAN='HDB18122022_144726' and MAHANG='HH003'

INSERT INTO HDBAN
VALUES ('HDB18122022_144726','NV001','12/18/2022','KH001',0)
INSERT INTO CHITIETHDBAN
VALUES ('HDB18122022_144726','HH003',1,20000,0,20000)

SELECT * FROM HDBAN
SELECT * FROM CHITIETHDBAN

--Cập nhật lại tổng tiền cho hóa đơn bán
CREATE TRIGGER UPDATE_TONGTIEN ON CHITIETHDBAN AFTER INSERT 
AS
	BEGIN
		DECLARE @TONG decimal(10,2)
		SELECT @TONG = (SELECT TONGTIEN FROM HDBAN WHERE MAHDBAN=(SELECT MAHDBAN FROM inserted))
		DECLARE @TONGMOI decimal(10,2)
		SELECT @TONGMOI=@TONG+(SELECT THANHTIEN FROM inserted)
		UPDATE HDBAN
		SET TONGTIEN= @TONGMOI WHERE MAHDBAN=(SELECT MAHDBAN FROM inserted)
	END
drop trigger UPDATE_TONGTIEN

INSERT INTO HDBAN
VALUES ('HDB18122022_144726','NV001','12/18/2022','KH001',0)
INSERT INTO CHITIETHDBAN
VALUES ('HDB18122022_144726','HH001',20,18000,0,360000)
INSERT INTO CHITIETHDBAN
VALUES ('HDB18122022_144726','HH002',2,20000,0,40000)
INSERT INTO CHITIETHDBAN
VALUES ('HDB18122022_144726','HH003',20,20000,0,400000)

select * from HANG
SELECT * FROM HDBAN
SELECT * FROM CHITIETHDBAN

-----------------------------------THỌ-----------------------------------
--Kiem tra don gia nhap phai lon hon 0
CREATE TRIGGER KT_DONGIANHAP ON HANG
FOR INSERT,UPDATE
AS
	IF(SELECT DONGIANHAP FROM INSERTED)>0
		COMMIT TRAN
ELSE
    BEGIN
        PRINT N'ĐƠN GIÁ NHẬP PHẢI LỚN HƠN 0'
        ROLLBACK TRAN
    END

--Kiểm tra số lượng mua phải lớn hơn hoặc bằng 100 thì mới được thêm Hóa đơn bán
CREATE TRIGGER KT_SOLUONGMUATREN100 ON CHITIETHDBAN
FOR INSERT,UPDATE
AS
	IF(SELECT SOLUONGMUA FROM INSERTED)>=100
    COMMIT TRAN
ELSE
    BEGIN
        PRINT N'SỐ LƯỢNG MUA PHẢI LỚN HƠN HOẶC BẰNG 100 THÌ MỚI ĐƯỢC XUẤT HÓA ĐƠN'
        ROLLBACK TRAN
    END

UPDATE CHITIETHDBAN
SET SOLUONGMUA=90
WHERE MAHDBAN='HDB18122022_144726'


select * from CHITIETHDBAN
select * from HDBAN
select * from HANG



drop proc insert_CTHD_Proc
select * from HDBAN
select * from CHITIETHDBAN where MAHDBAN='HDB12222022_135741'

-------------------------------------------DATA PHAN MEM
--------------------LOẠI HÀNG--------------------
---------------------------Thêm---------------------------
create proc sp_InsertLoaiHang
@maLoaiHang nvarchar(10), @tenLoaiHang nvarchar(40)
as
begin
	INSERT INTO LOAIHANG 
	VALUES(@maLoaiHang,@tenLoaiHang)
end

---------------------------Sửa---------------------------
create proc sp_UpdateLoaiHang
@tenLoaiHang nvarchar(40), @maLoaiHang nvarchar(10)
as
begin
	UPDATE LOAIHANG 
	SET TENLOAIHANG=@tenLoaiHang 
	WHERE MALOAIHANG=@maLoaiHang
end

---------------------------Xóa---------------------------
create proc sp_DeleteLoaiHang
@maLoaiHang nvarchar(10)
as
begin
	DELETE LOAIHANG 
	WHERE MALOAIHANG=@maLoaiHang
end


--------------------KHÁCH HÀNG--------------------
---------------------------Thêm---------------------------
create proc sp_InsertKhachHang
@maKhach nvarchar(10), @tenKhach nvarchar(40), @diaChi nvarchar(50), @dienThoai nvarchar(20)
as
begin
	INSERT INTO KHACH 
	VALUES (@maKhach, @tenKhach, @diaChi, @dienThoai)
end

---------------------------Sửa---------------------------
create proc sp_UpdateKhachHang
@maKhach nvarchar(10), @tenKhach nvarchar(40), @diaChi nvarchar(50), @dienThoai nvarchar(20)
as
begin
	UPDATE KHACH 
	SET TENKHACH=@tenKhach,DIACHI_KH=@diaChi,DIENTHOAI_KH=@dienThoai
	WHERE MAKHACH=@maKhach
end

---------------------------Xóa---------------------------
create proc sp_DeleteKhachHang
@maKhach nvarchar(10)
as
begin
	DELETE KHACH WHERE MAKHACH=@maKhach
end



--------------------HÀNG HÓA--------------------
---------------------------Thêm---------------------------
create proc sp_InsertHangHoa
@maHang nvarchar(10), @tenHang nvarchar(50), @soLuong int, @maLoaiHang nvarchar(10), @donGiaNhap float, @dongiaBan float
as
begin
	INSERT INTO HANG
	VALUES(@maHang,@tenHang,@soLuong,@maLoaiHang,@donGiaNhap,@dongiaBan)
end

---------------------------Sửa---------------------------
create proc sp_UpdateHangHoa
@tenHang nvarchar(50),  @maLoaiHang nvarchar(10), @soLuong int, @maHang nvarchar(10)
as
begin
	UPDATE HANG 
	SET TENHANG=@tenHang,MALOAIHANG=@maLoaiHang,SOLUONG=@soLuong 
	WHERE MAHANG=@maHang
end

---------------------------Xóa---------------------------
create proc sp_DeleteHangHoa
@maHang nvarchar(10)
as
begin
	DELETE HANG 
	WHERE MAHANG=@maHang
end

---------------------------Tìm tên hàng---------------------------
create proc sp_TimKiemTenHang
@tenHang nvarchar(50)
as
begin
	SELECT * FROM HANG WHERE 1=1 AND TENHANG LIKE @tenHang
end

--------------------Nhân Viên--------------------
---------------------------Thêm---------------------------
create proc sp_InsertNhanVien
@maNhanVien nvarchar(10), @tenNhanVien nvarchar(50), @gioiTinh nvarchar(5), @diaChi nvarchar(50), @dienThoai nvarchar(20), @ngaySinh datetime
as
begin
	INSERT INTO NHANVIEN(MANHANVIEN,TENNHANVIEN,GIOITINH, DIACHI_NV,DIENTHOAI_NV, NGAYSINH) 
	VALUES (@maNhanVien, @tenNhanVien, @gioiTinh, @diaChi, @dienThoai, @ngaySinh)
end

---------------------------Sửa---------------------------
create proc sp_UpdateNhanVien
@tenNhanVien nvarchar(50), @diaChi nvarchar(50), @dienThoai nvarchar(20), @gioiTinh nvarchar(5), @ngaySinh datetime, @maNhanVien nvarchar(10)
as
begin
	UPDATE NHANVIEN 
	SET TENNHANVIEN=@tenNhanVien,DIACHI_NV=@diaChi,DIENTHOAI_NV=@dienThoai,GIOITINH=@gioiTinh,NGAYSINH=@ngaySinh WHERE MANHANVIEN=@maNhanVien
end

---------------------------Xóa---------------------------
create proc sp_DeleteNhanVien
@maNhanVien nvarchar(10)
as
begin
	DELETE NHANVIEN 
	WHERE MANHANVIEN=@maNhanVien
end

---------------------------------------------------------THONG KE---------------------------------------------------------
------------Lấy Hóa Đơn Theo Ngày------------
create proc sp_LayHDTheoNgay
@fromDate datetime, @toDate datetime
as
begin
	select MAHDBAN, TENNHANVIEN, NGAYBAN, TENKHACH, TONGTIEN 
	from HDBAN, KHACH, NHANVIEN 
	where HDBAN.MANHANVIEN = NHANVIEN.MANHANVIEN and HDBAN.MAKHACH = KHACH.MAKHACH and NGAYBAN >= @fromDate AND NGAYBAN <= @toDate
end

------------Tính tổng doanh thu hóa đơn------------
create function TongAllHD()
returns int
as
	begin
		declare @tong int
		set @tong = (select sum(TONGTIEN) from HDBAN)
		return @tong
	end
go

------------Tính tổng doanh thu hóa đơn theo ngày------------
drop function dbo.TongHDTheoNgay	

create function TongHDTheoNgay(@fromDate datetime, @toDate datetime)
returns int
as
	begin
		declare @tong int
		declare @check varchar(5)
		set @check = (select sum(TONGTIEN) from HDBAN where NGAYBAN >= @fromDate AND NGAYBAN <= @toDate)
		if (@check is null)
			set @tong = 0
		else
			set @tong = (select sum(TONGTIEN) from HDBAN where NGAYBAN >= @fromDate AND NGAYBAN <= @toDate)
		return @tong
	end
go

select dbo.TongHDTheoNgay ('2022-12-23','2022-12-24')
------------Tổng số hóa đơn------------
create proc sp_tongSoHoaDon
as
begin
	select count(*) from HDBAN
end

------------Tổng số hóa đơn theo ngày------------

create proc sp_tongSoHDtheoNgay
@fromDate datetime, @toDate datetime
as
begin
	select count(*) from HDBAN where NGAYBAN >= @fromDate AND NGAYBAN <= @toDate
end


--HÓA ĐƠN
--Thêm và kiểm tra số lượng mua
create proc KTSLMua @mahd char(30),@mahang char(30),@slmua int,@dg decimal(10,2),@gg float,@tt decimal(10,2)
as
begin TRANSACTION 

INSERT INTO CHITIETHDBAN
VALUES (@mahd,@mahang,@slmua,@dg,@gg,@tt)

if(SELECT @slmua FROM CHITIETHDBAN WHERE MAHDBAN = @mahd and MAHANG=@mahang)>=(select SOLUONG from HANG where MAHANG=@mahang)
	
	begin
		print N'Số lượng mua lớn hơn số lượng hàng hiện có!'
		ROLLBACK
		RETURN
	end
COMMIT

drop proc KTSLMua
select * from CHITIETHDBAN where MAHDBAN='HDB12242022_100312'
exec KTSLMua 'HDB12242022_100312','HH009',2000,20000,0,4000
SELECT MAHDBAN,a.MAHANG, b.TENHANG, a.SOLUONGMUA, b.DONGIABAN, a.GIAMGIA,a.THANHTIEN FROM CHITIETHDBAN AS a, HANG AS b WHERE a.MAHDBAN = 'HDB12222022_135741' AND a.MAHANG=b.MAHANG
select * from HANG where MAHANG='HH001'
SELECT MAHANG FROM CHITIETHDBAN WHERE MAHDBAN='HDB12222022_150143'

--Xóa CHITIETHDBAN
create proc Delete_CTHDBAN_Func @mahdban char(30),@mahangxoa char(30)
as
begin
	DELETE CHITIETHDBAN WHERE MAHDBAN=@mahdban AND MAHANG = @mahangxoa
end
drop proc Delete_CTHDBAN_Func

exec Delete_CTHDBAN_Func 'HDB12232022_152912','HH001'
select * from CHITIETHDBAN where MAHDBAN='HDB12232022_154751' and MAHANG='HH001'

--Cập nhật lại số lượng cho các mặt hàng khi xóa số lượng hàng mua trong CTHD
CREATE TRIGGER UPDATE_XOAHANG ON CHITIETHDBAN AFTER DELETE 
AS
	BEGIN
		DECLARE @sl int
		declare @slxoa int
		declare @slcon int

		select @slxoa=(select SOLUONGMUA from deleted)
		select @sl=(select SOLUONG from HANG JOIN deleted ON HANG.MAHANG=deleted.MAHANG)
		select @slcon=@sl+@slxoa

		UPDATE HANG
		SET SOLUONG= @slcon from HANG join deleted on HANG.MAHANG=deleted.MAHANG
	END

select * from HANG where MAHANG='HH001'

--Cập nhật lại tổng tiền cho hóa đơn bán khi xóa số lượng hàng mua trong CTHD
CREATE TRIGGER UPDATE_TONGTIEN_AFTER_XOAHANG ON CHITIETHDBAN AFTER DELETE 
AS
	BEGIN
		DECLARE @tong float
		declare @tongtienxoa int
		declare @tongmoi int
		declare @thanhtienxoa float

		select @tong=(SELECT TONGTIEN FROM HDBAN JOIN deleted ON HDBAN.MAHDBAN=deleted.MAHDBAN)
		select @thanhtienxoa=(select THANHTIEN from deleted)
		select @tongmoi=@tong-@thanhtienxoa

		UPDATE HDBAN
		SET TONGTIEN= @tongmoi from HDBAN join deleted on HDBAN.MAHDBAN=deleted.MAHDBAN
	END

select TONGTIEN from HDBAN where MAHDBAN='HDB12242022_134117'
select * from CHITIETHDBAN where MAHDBAN='HDB12242022_134117'

select TONGTIEN from HDBAN where HDBAN.MAHDBAN='HDB12242022_142011'

select * from HANG where MAHANG='HH001'
select * from HANG where MAHANG='HH002'


SELECT * FROM HDBAN WHERE MAHDBAN = 'HDB12242022_191243'
