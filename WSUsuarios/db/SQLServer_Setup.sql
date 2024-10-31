USE [master]
GO
/****** Object:  Database [PagosMovilesBancario]    Script Date: 14/10/2024 09:46:14 a. m. ******/
CREATE DATABASE [PagosMovilesBancario]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PagosMovilesBancario', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\PagosMovilesBancario.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PagosMovilesBancario_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\PagosMovilesBancario_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [PagosMovilesBancario] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PagosMovilesBancario].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PagosMovilesBancario] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PagosMovilesBancario] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PagosMovilesBancario] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PagosMovilesBancario] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PagosMovilesBancario] SET ARITHABORT OFF 
GO
ALTER DATABASE [PagosMovilesBancario] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PagosMovilesBancario] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PagosMovilesBancario] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PagosMovilesBancario] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PagosMovilesBancario] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PagosMovilesBancario] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PagosMovilesBancario] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PagosMovilesBancario] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PagosMovilesBancario] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PagosMovilesBancario] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PagosMovilesBancario] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PagosMovilesBancario] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PagosMovilesBancario] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PagosMovilesBancario] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PagosMovilesBancario] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PagosMovilesBancario] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PagosMovilesBancario] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PagosMovilesBancario] SET RECOVERY FULL 
GO
ALTER DATABASE [PagosMovilesBancario] SET  MULTI_USER 
GO
ALTER DATABASE [PagosMovilesBancario] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PagosMovilesBancario] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PagosMovilesBancario] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PagosMovilesBancario] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PagosMovilesBancario] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PagosMovilesBancario] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'PagosMovilesBancario', N'ON'
GO
ALTER DATABASE [PagosMovilesBancario] SET QUERY_STORE = ON
GO
ALTER DATABASE [PagosMovilesBancario] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [PagosMovilesBancario]
GO
/****** Object:  Table [dbo].[Cuentas]    Script Date: 14/10/2024 09:46:14 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuentas](
	[id_cuenta] [int] IDENTITY(1,1) NOT NULL,
	[identificacion] [varchar](20) NOT NULL,
	[numero_cuenta] [varchar](20) NOT NULL,
	[saldo] [decimal](15, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_cuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cuentas] ON 

INSERT [dbo].[Cuentas] ([id_cuenta], [identificacion], [numero_cuenta], [saldo]) VALUES (1, N'101110111', N'1234567890', CAST(36740.00 AS Decimal(15, 2)))
INSERT [dbo].[Cuentas] ([id_cuenta], [identificacion], [numero_cuenta], [saldo]) VALUES (2, N'202220222', N'9876543210', CAST(570.00 AS Decimal(15, 2)))
INSERT [dbo].[Cuentas] ([id_cuenta], [identificacion], [numero_cuenta], [saldo]) VALUES (3, N'303330333', N'1122334455', CAST(1660.75 AS Decimal(15, 2)))
INSERT [dbo].[Cuentas] ([id_cuenta], [identificacion], [numero_cuenta], [saldo]) VALUES (4, N'404440444', N'6677889900', CAST(980.50 AS Decimal(15, 2)))
INSERT [dbo].[Cuentas] ([id_cuenta], [identificacion], [numero_cuenta], [saldo]) VALUES (5, N'505550555', N'9988776655', CAST(92140.20 AS Decimal(15, 2)))
INSERT [dbo].[Cuentas] ([id_cuenta], [identificacion], [numero_cuenta], [saldo]) VALUES (6, N'606660666', N'5544332211', CAST(3565.00 AS Decimal(15, 2)))
INSERT [dbo].[Cuentas] ([id_cuenta], [identificacion], [numero_cuenta], [saldo]) VALUES (7, N'707770777', N'1234987654', CAST(452.45 AS Decimal(15, 2)))
INSERT [dbo].[Cuentas] ([id_cuenta], [identificacion], [numero_cuenta], [saldo]) VALUES (8, N'808880888', N'8765432109', CAST(1280.90 AS Decimal(15, 2)))
INSERT [dbo].[Cuentas] ([id_cuenta], [identificacion], [numero_cuenta], [saldo]) VALUES (9, N'909990999', N'1029384756', CAST(5000.00 AS Decimal(15, 2)))
INSERT [dbo].[Cuentas] ([id_cuenta], [identificacion], [numero_cuenta], [saldo]) VALUES (10, N'111110000', N'1928374650', CAST(100.00 AS Decimal(15, 2)))
SET IDENTITY_INSERT [dbo].[Cuentas] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Cuentas__C6B74B880D95BD53]    Script Date: 14/10/2024 09:46:14 a. m. ******/
ALTER TABLE [dbo].[Cuentas] ADD UNIQUE NONCLUSTERED 
(
	[numero_cuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [PagosMovilesBancario] SET  READ_WRITE 
GO
