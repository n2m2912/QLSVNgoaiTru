USE [master]
GO
/****** Object:  Database [sinhvien]    Script Date: 03/21/2019 8:51:01 AM ******/
CREATE DATABASE [sinhvien]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'sinhvien', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\sinhvien.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'sinhvien_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\sinhvien_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [sinhvien] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [sinhvien].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [sinhvien] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [sinhvien] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [sinhvien] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [sinhvien] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [sinhvien] SET ARITHABORT OFF 
GO
ALTER DATABASE [sinhvien] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [sinhvien] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [sinhvien] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [sinhvien] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [sinhvien] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [sinhvien] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [sinhvien] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [sinhvien] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [sinhvien] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [sinhvien] SET  DISABLE_BROKER 
GO
ALTER DATABASE [sinhvien] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [sinhvien] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [sinhvien] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [sinhvien] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [sinhvien] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [sinhvien] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [sinhvien] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [sinhvien] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [sinhvien] SET  MULTI_USER 
GO
ALTER DATABASE [sinhvien] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [sinhvien] SET DB_CHAINING OFF 
GO
ALTER DATABASE [sinhvien] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [sinhvien] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [sinhvien] SET DELAYED_DURABILITY = DISABLED 
GO
USE [sinhvien]
GO
/****** Object:  Table [dbo].[bienbanvipham]    Script Date: 03/21/2019 8:51:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bienbanvipham](
	[Mabienbanvipham] [int] IDENTITY(1,1) NOT NULL,
	[Solanvp] [int] NULL,
	[Machunhatro] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Mabienbanvipham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[bienbanxulivipham]    Script Date: 03/21/2019 8:51:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bienbanxulivipham](
	[Mabienbanxuly] [int] IDENTITY(1,1) NOT NULL,
	[Noidung] [nvarchar](500) NULL,
	[Masv] [int] NOT NULL,
	[Machunhatro] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Mabienbanxuly] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[biennhancoc]    Script Date: 03/21/2019 8:51:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[biennhancoc](
	[MaBNtiencoc] [int] IDENTITY(1,1) NOT NULL,
	[Ngaynhancoc] [date] NULL,
	[Machunhatro] [int] NOT NULL,
	[Masv] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaBNtiencoc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[biennhanphisinhhoat]    Script Date: 03/21/2019 8:51:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[biennhanphisinhhoat](
	[Mabiennhanphisinhhoat] [int] IDENTITY(1,1) NOT NULL,
	[Ngaydongtien] [date] NULL,
	[Cackhoandathu] [nvarchar](500) NULL,
	[Maphongtro] [int] NOT NULL,
	[Machunhatro] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Mabiennhanphisinhhoat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[chunhatro]    Script Date: 03/21/2019 8:51:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[chunhatro](
	[Machunhatro] [int] NOT NULL,
	[Tenchunhatro] [nvarchar](500) NULL,
	[SDT] [int] NULL,
	[Hokhau] [nvarchar](500) NULL,
	[Pass] [varchar](500) NOT NULL,
	[Imagees] [nvarchar](500) NULL,
 CONSTRAINT [PK__chunhatr__B01DB71FB9DEAD9A] PRIMARY KEY CLUSTERED 
(
	[Machunhatro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ctbbvp]    Script Date: 03/21/2019 8:51:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ctbbvp](
	[Masv] [int] NOT NULL,
	[Mabienbanvipham] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Masv] ASC,
	[Mabienbanvipham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ctdv]    Script Date: 03/21/2019 8:51:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ctdv](
	[MaloaiDV] [int] NOT NULL,
	[Mabiennhanphisinhhoat] [int] NOT NULL,
	[Soluong] [int] NULL,
	[Thanhtien] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaloaiDV] ASC,
	[Mabiennhanphisinhhoat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ctlvp]    Script Date: 03/21/2019 8:51:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ctlvp](
	[MaloaiVP] [int] NOT NULL,
	[Mabienbanvipham] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaloaiVP] ASC,
	[Mabienbanvipham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ctpbtddcnt]    Script Date: 03/21/2019 8:51:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ctpbtddcnt](
	[Magiaybaothaydoidiachi] [int] NOT NULL,
	[Maquanhuyen] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Magiaybaothaydoidiachi] ASC,
	[Maquanhuyen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[dichvu]    Script Date: 03/21/2019 8:51:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[dichvu](
	[MaloaiDV] [int] IDENTITY(1,1) NOT NULL,
	[TenloaiDV] [nvarchar](500) NULL,
	[Donvitinh] [nvarchar](500) NULL,
	[Dongia] [float] NULL,
	[Hinhthucthanhtoan] [nvarchar](500) NULL,
	[Thoidiemthanhtoan] [nvarchar](500) NULL,
	[Machunhatro] [int] NOT NULL,
 CONSTRAINT [PK__dichvu__F82E44D01A35F7FE] PRIMARY KEY CLUSTERED 
(
	[MaloaiDV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[khoa]    Script Date: 03/21/2019 8:51:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[khoa](
	[Makhoa] [int] IDENTITY(1,1) NOT NULL,
	[Tenkhoa] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[Makhoa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[loaivp]    Script Date: 03/21/2019 8:51:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[loaivp](
	[MaloaiVP] [int] IDENTITY(1,1) NOT NULL,
	[TenloaiVP] [nvarchar](500) NULL,
	[Hinhthucxuly] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaloaiVP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[lop]    Script Date: 03/21/2019 8:51:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[lop](
	[Malop] [int] IDENTITY(1,1) NOT NULL,
	[Tenlop] [varchar](500) NULL,
	[Makhoa] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Malop] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[nguoidung]    Script Date: 03/21/2019 8:51:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[nguoidung](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[userpassword] [varchar](255) NOT NULL,
	[username] [varchar](255) NOT NULL,
	[Images] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[phieubaothaydoidiachingoaitru]    Script Date: 03/21/2019 8:51:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[phieubaothaydoidiachingoaitru](
	[Magiaybaothaydoidiachi] [int] IDENTITY(1,1) NOT NULL,
	[Diachicu] [nvarchar](500) NULL,
	[Diachimoi] [nvarchar](500) NULL,
	[Ngaythaydoidiachi] [date] NULL,
	[SDT] [varchar](10) NULL,
	[Masv] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Magiaybaothaydoidiachi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[phieudangkingoaitru]    Script Date: 03/21/2019 8:51:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[phieudangkingoaitru](
	[MaPDKNT] [int] IDENTITY(1,1) NOT NULL,
	[Ngaylapphieu] [date] NULL,
	[Masv] [int] NOT NULL,
	[Maquanhuyen] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaPDKNT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[phieudangkitimphongtro]    Script Date: 03/21/2019 8:51:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[phieudangkitimphongtro](
	[Madangkytimnhatro] [int] IDENTITY(1,1) NOT NULL,
	[Ngaylapphieu] [date] NULL,
	[Doituonguutien] [nvarchar](500) NULL,
	[SDTgiadinh] [varchar](10) NULL,
	[Masv] [int] NOT NULL,
	[Maphongtro] [int] NOT NULL,
	[Maquanhuyen] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Madangkytimnhatro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[phieudongtienphong]    Script Date: 03/21/2019 8:51:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[phieudongtienphong](
	[Maphieudongtienphong] [int] IDENTITY(1,1) NOT NULL,
	[Ngaylapphieu] [date] NULL,
	[Doituonguutien] [nvarchar](500) NULL,
	[SDTgiadinh] [nvarchar](500) NULL,
	[Machunhatro] [int] NOT NULL,
	[Maphongtro] [int] NOT NULL,
	[Tienphong] [float] NULL
PRIMARY KEY CLUSTERED 
(
	[Maphieudongtienphong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[phieugioithieusinhvienngoaitru]    Script Date: 03/21/2019 8:51:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[phieugioithieusinhvienngoaitru](
	[Maxacnhan] [int] IDENTITY(1,1) NOT NULL,
	[Conganphuongxa] [nvarchar](500) NULL,
	[Maquanhuyen] [int] NULL,
	[Masv] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Maxacnhan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[phieuthaydoidiachi]    Script Date: 03/21/2019 8:51:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[phieuthaydoidiachi](
	[Maphieuthaydoidiachi] [int] IDENTITY(1,1) NOT NULL,
	[Masv] [int] NOT NULL,
	[Tensv] [nvarchar](500) NULL,
	[SDT] [varchar](10) NULL,
	[Diachicu] [nvarchar](500) NULL,
	[Diachimoi] [nvarchar](500) NULL,
	[Ngaythaydoidiachi] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[Maphieuthaydoidiachi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[phieuxacnhanthongtinngoaitru]    Script Date: 03/21/2019 8:51:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[phieuxacnhanthongtinngoaitru](
	[Maphieuxacnhan] [int] IDENTITY(1,1) NOT NULL,
	[Conganphuongxa] [nvarchar](500) NULL,
	[Maquanhuyen] [int] NULL,
	[Diachicutru] [nvarchar](500) NULL,
	[Todanpho] [nvarchar](500) NULL,
	[Quanhevoichuho] [nvarchar](500) NULL,
	[Nhanxetsinhvien] [nvarchar](500) NULL,
	[Masv] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Maphieuxacnhan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[phongtro]    Script Date: 03/21/2019 8:51:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[phongtro](
	[Maphongtro] [int] IDENTITY(1,1) NOT NULL,
	[Tiendatcoc] [float] NULL,
	[Sophong] [int] NULL,
	[Noiquyphongtro] [nvarchar](500) NULL,
	[Machunhatro] [int] NULL,
	[Images] [varchar](500) NULL,
	[Googlemap] [nvarchar](500) NOT NULL,
	[Tenphong] [nvarchar](50) NULL,
	[Mota] [nvarchar](500) NULL,
 CONSTRAINT [PK__phongtro__1074B0CB7EFC1315] PRIMARY KEY CLUSTERED 
(
	[Maphongtro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[quanhuyen]    Script Date: 03/21/2019 8:51:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[quanhuyen](
	[Maquanhuyen] [int] IDENTITY(1,1) NOT NULL,
	[Tenquanhuyen] [nvarchar](500) NULL,
	[Matinhtp] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Maquanhuyen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sinhvien]    Script Date: 03/21/2019 8:51:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sinhvien](
	[Masv] [int] NOT NULL,
	[Tensv] [nvarchar](50) NULL,
	[Gioitinh] [nvarchar](500) NULL,
	[Diachi] [nvarchar](500) NULL,
	[SDT] [varchar](10) NULL,
	[Quequan] [nvarchar](500) NULL,
	[Nienkhoa] [varchar](500) NULL,
	[Malop] [int] NULL,
	[Pass] [varchar](50) NULL,
	[Images] [nvarchar](500) NULL,
 CONSTRAINT [PK__sinhvien__27240C226B71DA89] PRIMARY KEY CLUSTERED 
(
	[Masv] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tinhtp]    Script Date: 03/21/2019 8:51:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tinhtp](
	[Matinhtp] [int] IDENTITY(1,1) NOT NULL,
	[Tentinhtp] [nvarchar](500) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Matinhtp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[bienbanvipham]  WITH CHECK ADD  CONSTRAINT [FK__bienbanvi__Machu__3A81B327] FOREIGN KEY([Machunhatro])
REFERENCES [dbo].[chunhatro] ([Machunhatro])
GO
ALTER TABLE [dbo].[bienbanvipham] CHECK CONSTRAINT [FK__bienbanvi__Machu__3A81B327]
GO
ALTER TABLE [dbo].[bienbanxulivipham]  WITH CHECK ADD  CONSTRAINT [FK__bienbanxu__Machu__3C69FB99] FOREIGN KEY([Machunhatro])
REFERENCES [dbo].[chunhatro] ([Machunhatro])
GO
ALTER TABLE [dbo].[bienbanxulivipham] CHECK CONSTRAINT [FK__bienbanxu__Machu__3C69FB99]
GO
ALTER TABLE [dbo].[bienbanxulivipham]  WITH CHECK ADD  CONSTRAINT [FK__bienbanxul__Masv__3B75D760] FOREIGN KEY([Masv])
REFERENCES [dbo].[sinhvien] ([Masv])
GO
ALTER TABLE [dbo].[bienbanxulivipham] CHECK CONSTRAINT [FK__bienbanxul__Masv__3B75D760]
GO
ALTER TABLE [dbo].[biennhancoc]  WITH CHECK ADD  CONSTRAINT [FK__biennhanc__Machu__3E52440B] FOREIGN KEY([Machunhatro])
REFERENCES [dbo].[chunhatro] ([Machunhatro])
GO
ALTER TABLE [dbo].[biennhancoc] CHECK CONSTRAINT [FK__biennhanc__Machu__3E52440B]
GO
ALTER TABLE [dbo].[biennhancoc]  WITH CHECK ADD  CONSTRAINT [FK__biennhanco__Masv__3D5E1FD2] FOREIGN KEY([Masv])
REFERENCES [dbo].[sinhvien] ([Masv])
GO
ALTER TABLE [dbo].[biennhancoc] CHECK CONSTRAINT [FK__biennhanco__Masv__3D5E1FD2]
GO
ALTER TABLE [dbo].[biennhanphisinhhoat]  WITH CHECK ADD  CONSTRAINT [FK__biennhanp__Machu__3F466844] FOREIGN KEY([Machunhatro])
REFERENCES [dbo].[chunhatro] ([Machunhatro])
GO
ALTER TABLE [dbo].[biennhanphisinhhoat] CHECK CONSTRAINT [FK__biennhanp__Machu__3F466844]
GO
ALTER TABLE [dbo].[biennhanphisinhhoat]  WITH CHECK ADD  CONSTRAINT [FK__biennhanp__Mapho__403A8C7D] FOREIGN KEY([Maphongtro])
REFERENCES [dbo].[phongtro] ([Maphongtro])
GO
ALTER TABLE [dbo].[biennhanphisinhhoat] CHECK CONSTRAINT [FK__biennhanp__Mapho__403A8C7D]
GO
ALTER TABLE [dbo].[ctbbvp]  WITH CHECK ADD FOREIGN KEY([Mabienbanvipham])
REFERENCES [dbo].[bienbanvipham] ([Mabienbanvipham])
GO
ALTER TABLE [dbo].[ctbbvp]  WITH CHECK ADD  CONSTRAINT [FK__ctbbvp__Masv__412EB0B6] FOREIGN KEY([Masv])
REFERENCES [dbo].[sinhvien] ([Masv])
GO
ALTER TABLE [dbo].[ctbbvp] CHECK CONSTRAINT [FK__ctbbvp__Masv__412EB0B6]
GO
ALTER TABLE [dbo].[ctdv]  WITH CHECK ADD FOREIGN KEY([Mabiennhanphisinhhoat])
REFERENCES [dbo].[biennhanphisinhhoat] ([Mabiennhanphisinhhoat])
GO
ALTER TABLE [dbo].[ctdv]  WITH CHECK ADD  CONSTRAINT [FK__ctdv__MaloaiDV__440B1D61] FOREIGN KEY([MaloaiDV])
REFERENCES [dbo].[dichvu] ([MaloaiDV])
GO
ALTER TABLE [dbo].[dichvu]  WITH CHECK ADD  CONSTRAINT [FK__dv__Machunhatro__440B1D61] FOREIGN KEY([Machunhatro])
REFERENCES [dbo].[chunhatro] ([Machunhatro])
GO
ALTER TABLE [dbo].[ctdv] CHECK CONSTRAINT [FK__ctdv__MaloaiDV__440B1D61]
GO
ALTER TABLE [dbo].[ctlvp]  WITH CHECK ADD FOREIGN KEY([Mabienbanvipham])
REFERENCES [dbo].[bienbanvipham] ([Mabienbanvipham])
GO
ALTER TABLE [dbo].[ctlvp]  WITH CHECK ADD FOREIGN KEY([MaloaiVP])
REFERENCES [dbo].[loaivp] ([MaloaiVP])
GO
ALTER TABLE [dbo].[ctpbtddcnt]  WITH CHECK ADD FOREIGN KEY([Magiaybaothaydoidiachi])
REFERENCES [dbo].[phieubaothaydoidiachingoaitru] ([Magiaybaothaydoidiachi])
GO
ALTER TABLE [dbo].[ctpbtddcnt]  WITH CHECK ADD FOREIGN KEY([Maquanhuyen])
REFERENCES [dbo].[quanhuyen] ([Maquanhuyen])
GO
ALTER TABLE [dbo].[lop]  WITH CHECK ADD FOREIGN KEY([Makhoa])
REFERENCES [dbo].[khoa] ([Makhoa])
GO
ALTER TABLE [dbo].[phieubaothaydoidiachingoaitru]  WITH CHECK ADD  CONSTRAINT [FK__phieubaoth__Masv__49C3F6B7] FOREIGN KEY([Masv])
REFERENCES [dbo].[sinhvien] ([Masv])
GO
ALTER TABLE [dbo].[phieubaothaydoidiachingoaitru] CHECK CONSTRAINT [FK__phieubaoth__Masv__49C3F6B7]
GO
ALTER TABLE [dbo].[phieudangkingoaitru]  WITH CHECK ADD FOREIGN KEY([Maquanhuyen])
REFERENCES [dbo].[quanhuyen] ([Maquanhuyen])
GO
ALTER TABLE [dbo].[phieudangkingoaitru]  WITH CHECK ADD  CONSTRAINT [FK__phieudangk__Masv__4AB81AF0] FOREIGN KEY([Masv])
REFERENCES [dbo].[sinhvien] ([Masv])
GO
ALTER TABLE [dbo].[phieudangkingoaitru] CHECK CONSTRAINT [FK__phieudangk__Masv__4AB81AF0]
GO
ALTER TABLE [dbo].[phieudangkitimphongtro]  WITH CHECK ADD  CONSTRAINT [FK__phieudang__Mapho__5AEE82B9] FOREIGN KEY([Maphongtro])
REFERENCES [dbo].[phongtro] ([Maphongtro])
GO
ALTER TABLE [dbo].[phieudangkitimphongtro] CHECK CONSTRAINT [FK__phieudang__Mapho__5AEE82B9]
GO
ALTER TABLE [dbo].[phieudangkitimphongtro]  WITH CHECK ADD FOREIGN KEY([Maquanhuyen])
REFERENCES [dbo].[quanhuyen] ([Maquanhuyen])
GO
ALTER TABLE [dbo].[phieudangkitimphongtro]  WITH CHECK ADD  CONSTRAINT [FK__phieudangk__Masv__5629CD9C] FOREIGN KEY([Masv])
REFERENCES [dbo].[sinhvien] ([Masv])
GO
ALTER TABLE [dbo].[phieudangkitimphongtro] CHECK CONSTRAINT [FK__phieudangk__Masv__5629CD9C]
GO
ALTER TABLE [dbo].[phieudongtienphong]  WITH CHECK ADD  CONSTRAINT [FK__phieudong__Machu__4D94879B] FOREIGN KEY([Machunhatro])
REFERENCES [dbo].[chunhatro] ([Machunhatro])
GO
ALTER TABLE [dbo].[phieudongtienphong] CHECK CONSTRAINT [FK__phieudong__Machu__4D94879B]
GO
ALTER TABLE [dbo].[phieudongtienphong]  WITH CHECK ADD  CONSTRAINT [FK__phieudong__Mapho__4CA06362] FOREIGN KEY([Maphongtro])
REFERENCES [dbo].[phongtro] ([Maphongtro])
GO
ALTER TABLE [dbo].[phieudongtienphong] CHECK CONSTRAINT [FK__phieudong__Mapho__4CA06362]
GO
ALTER TABLE [dbo].[phieugioithieusinhvienngoaitru]  WITH CHECK ADD FOREIGN KEY([Maquanhuyen])
REFERENCES [dbo].[quanhuyen] ([Maquanhuyen])
GO
ALTER TABLE [dbo].[phieugioithieusinhvienngoaitru]  WITH CHECK ADD  CONSTRAINT [FK__phieugioit__Masv__4E88ABD4] FOREIGN KEY([Masv])
REFERENCES [dbo].[sinhvien] ([Masv])
GO
ALTER TABLE [dbo].[phieugioithieusinhvienngoaitru] CHECK CONSTRAINT [FK__phieugioit__Masv__4E88ABD4]
GO
ALTER TABLE [dbo].[phieuthaydoidiachi]  WITH CHECK ADD  CONSTRAINT [FK__phieuthayd__Masv__5535A963] FOREIGN KEY([Masv])
REFERENCES [dbo].[sinhvien] ([Masv])
GO
ALTER TABLE [dbo].[phieuthaydoidiachi] CHECK CONSTRAINT [FK__phieuthayd__Masv__5535A963]
GO
ALTER TABLE [dbo].[phieuxacnhanthongtinngoaitru]  WITH CHECK ADD FOREIGN KEY([Maquanhuyen])
REFERENCES [dbo].[quanhuyen] ([Maquanhuyen])
GO
ALTER TABLE [dbo].[phieuxacnhanthongtinngoaitru]  WITH CHECK ADD  CONSTRAINT [FK__phieuxacnh__Masv__5070F446] FOREIGN KEY([Masv])
REFERENCES [dbo].[sinhvien] ([Masv])
GO
ALTER TABLE [dbo].[phieuxacnhanthongtinngoaitru] CHECK CONSTRAINT [FK__phieuxacnh__Masv__5070F446]
GO
ALTER TABLE [dbo].[phongtro]  WITH CHECK ADD  CONSTRAINT [FK__phongtro__Machun__52593CB8] FOREIGN KEY([Machunhatro])
REFERENCES [dbo].[chunhatro] ([Machunhatro])
GO
ALTER TABLE [dbo].[phongtro] CHECK CONSTRAINT [FK__phongtro__Machun__52593CB8]
GO
ALTER TABLE [dbo].[quanhuyen]  WITH CHECK ADD FOREIGN KEY([Matinhtp])
REFERENCES [dbo].[tinhtp] ([Matinhtp])
GO
ALTER TABLE [dbo].[sinhvien]  WITH CHECK ADD  CONSTRAINT [FK__sinhvien__Malop__5441852A] FOREIGN KEY([Malop])
REFERENCES [dbo].[lop] ([Malop])
GO
ALTER TABLE [dbo].[sinhvien] CHECK CONSTRAINT [FK__sinhvien__Malop__5441852A]
GO
USE [master]
GO
ALTER DATABASE [sinhvien] SET  READ_WRITE 
GO
