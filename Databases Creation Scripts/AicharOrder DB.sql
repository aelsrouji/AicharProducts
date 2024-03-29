USE [master]
GO
/****** Object:  Database [AicharOrder]    Script Date: 3/27/2022 11:15:44 AM ******/
CREATE DATABASE [AicharOrder]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AicharOrder', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\AicharOrder.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AicharOrder_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\AicharOrder_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [AicharOrder] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AicharOrder].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AicharOrder] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AicharOrder] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AicharOrder] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AicharOrder] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AicharOrder] SET ARITHABORT OFF 
GO
ALTER DATABASE [AicharOrder] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AicharOrder] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AicharOrder] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AicharOrder] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AicharOrder] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AicharOrder] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AicharOrder] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AicharOrder] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AicharOrder] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AicharOrder] SET  ENABLE_BROKER 
GO
ALTER DATABASE [AicharOrder] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AicharOrder] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AicharOrder] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AicharOrder] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AicharOrder] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AicharOrder] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [AicharOrder] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AicharOrder] SET RECOVERY FULL 
GO
ALTER DATABASE [AicharOrder] SET  MULTI_USER 
GO
ALTER DATABASE [AicharOrder] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AicharOrder] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AicharOrder] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AicharOrder] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AicharOrder] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AicharOrder] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'AicharOrder', N'ON'
GO
ALTER DATABASE [AicharOrder] SET QUERY_STORE = OFF
GO
USE [AicharOrder]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 3/27/2022 11:15:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 3/27/2022 11:15:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[OrderHeaderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Count] [int] NOT NULL,
	[ProductName] [nvarchar](max) NULL,
	[ProductPrice] [float] NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderHeaders]    Script Date: 3/27/2022 11:15:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderHeaders](
	[OrderHeaderId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](max) NULL,
	[CouponCode] [nvarchar](max) NULL,
	[OrderTotal] [float] NOT NULL,
	[DiscountTotal] [float] NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[PickupDatetime] [datetime2](7) NOT NULL,
	[OrderTime] [datetime2](7) NOT NULL,
	[Phone] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[CardNumber] [nvarchar](max) NULL,
	[CVV] [nvarchar](max) NULL,
	[ExpirtyMonthYear] [nvarchar](max) NULL,
	[CartTotalItems] [int] NOT NULL,
	[PaymentStatus] [bit] NOT NULL,
 CONSTRAINT [PK_OrderHeaders] PRIMARY KEY CLUSTERED 
(
	[OrderHeaderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderDetails_OrderHeaderId]    Script Date: 3/27/2022 11:15:44 AM ******/
CREATE NONCLUSTERED INDEX [IX_OrderDetails_OrderHeaderId] ON [dbo].[OrderDetails]
(
	[OrderHeaderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_OrderHeaders_OrderHeaderId] FOREIGN KEY([OrderHeaderId])
REFERENCES [dbo].[OrderHeaders] ([OrderHeaderId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_OrderHeaders_OrderHeaderId]
GO
USE [master]
GO
ALTER DATABASE [AicharOrder] SET  READ_WRITE 
GO
