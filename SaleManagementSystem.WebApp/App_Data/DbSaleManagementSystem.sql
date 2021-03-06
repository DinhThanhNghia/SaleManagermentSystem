USE [master]
GO
/****** Object:  Database [SaleManagementSystem]    Script Date: 11/17/2018 7:54:07 PM ******/
CREATE DATABASE [SaleManagementSystem]
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SaleManagementSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SaleManagementSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SaleManagementSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SaleManagementSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SaleManagementSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SaleManagementSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [SaleManagementSystem] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [SaleManagementSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SaleManagementSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SaleManagementSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SaleManagementSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SaleManagementSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SaleManagementSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SaleManagementSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SaleManagementSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SaleManagementSystem] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SaleManagementSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SaleManagementSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SaleManagementSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SaleManagementSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SaleManagementSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SaleManagementSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SaleManagementSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SaleManagementSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SaleManagementSystem] SET  MULTI_USER 
GO
ALTER DATABASE [SaleManagementSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SaleManagementSystem] SET DB_CHAINING OFF 
GO
USE [SaleManagementSystem]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 11/17/2018 7:54:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](200) NULL,
	[Phone] [varchar](20) NOT NULL,
	[JoinDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[InsAt] [datetime] NOT NULL,
	[InsBy] [nvarchar](50) NOT NULL,
	[UpdAt] [datetime] NOT NULL,
	[UpdBy] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Delivery]    Script Date: 11/17/2018 7:54:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Delivery](
	[Id] [uniqueidentifier] NOT NULL,
	[SaleId] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NOT NULL,
	[DeliveriedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[InsAt] [datetime] NOT NULL,
	[InsBy] [nvarchar](50) NOT NULL,
	[UpdAt] [datetime] NOT NULL,
	[UpdBy] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 11/17/2018 7:54:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Phone] [varchar](20) NULL,
	[JoinDate] [datetime] NOT NULL,
	[LeftDate] [datetime] NULL,
	[Position] [nvarchar](100) NULL,
	[Salary] [money] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[InsAt] [datetime] NOT NULL,
	[InsBy] [nvarchar](50) NOT NULL,
	[UpdAt] [datetime] NOT NULL,
	[UpdBy] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Item]    Script Date: 11/17/2018 7:54:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Unit] [nvarchar](100) NOT NULL,
	[Amount] [money] NULL,
	[DateExpired] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[InsAt] [datetime] NOT NULL,
	[InsBy] [nvarchar](50) NOT NULL,
	[UpdAt] [datetime] NOT NULL,
	[UpdBy] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuItem]    Script Date: 11/17/2018 7:54:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuItem](
	[Id] [uniqueidentifier] NOT NULL,
	[ItemId] [uniqueidentifier] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUsedDate] [datetime] NOT NULL,
	[Rating] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[InsAt] [datetime] NOT NULL,
	[InsBy] [nvarchar](50) NOT NULL,
	[UpdAt] [datetime] NOT NULL,
	[UpdBy] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PickUp]    Script Date: 11/17/2018 7:54:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PickUp](
	[Id] [uniqueidentifier] NOT NULL,
	[SupplierId] [uniqueidentifier] NOT NULL,
	[EmployeeId] [uniqueidentifier] NOT NULL,
	[PickUpDate] [datetime] NOT NULL,
	[Price] [money] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Address] [nvarchar](100) NULL,
	[Unit] [nvarchar](100) NULL,
	[IsDeleted] [bit] NOT NULL,
	[InsAt] [datetime] NOT NULL,
	[InsBy] [nvarchar](50) NOT NULL,
	[UpdAt] [datetime] NOT NULL,
	[UpdBy] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Property]    Script Date: 11/17/2018 7:54:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Property](
	[Id] [uniqueidentifier] NOT NULL,
	[ItemId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[Status] [nvarchar](100) NULL,
	[OwnedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[InsAt] [datetime] NOT NULL,
	[InsBy] [nvarchar](50) NOT NULL,
	[UpdAt] [datetime] NOT NULL,
	[UpdBy] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purchase]    Script Date: 11/17/2018 7:54:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchase](
	[Id] [uniqueidentifier] NOT NULL,
	[SupplierId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[Price] [money] NOT NULL,
	[PurchaseDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[InsAt] [datetime] NOT NULL,
	[InsBy] [nvarchar](50) NOT NULL,
	[UpdAt] [datetime] NOT NULL,
	[UpdBy] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sale]    Script Date: 11/17/2018 7:54:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sale](
	[Id] [uniqueidentifier] NOT NULL,
	[EmployeeId] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[SaleCode] [varchar](50) NOT NULL,
	[Unit] [varchar](50) NOT NULL,
	[Price] [money] NOT NULL,
	[ItemId] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[InsAt] [datetime] NOT NULL,
	[InsBy] [nvarchar](50) NOT NULL,
	[UpdAt] [datetime] NOT NULL,
	[UpdBy] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 11/17/2018 7:54:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Rating] [int] NULL,
	[ContactNo] [varchar](20) NOT NULL,
	[ProvideDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[InsAt] [datetime] NOT NULL,
	[InsBy] [nvarchar](50) NOT NULL,
	[UpdAt] [datetime] NOT NULL,
	[UpdBy] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Customer] ([Id], [Name], [Address], [Phone], [JoinDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'1ceef734-8fb9-4a0c-be01-14e2cb8acbc8', N'Lương Xuân Trường', N'49 Trịnh Công Sơn, Đà Nẵng', N'0241534789', CAST(N'2018-11-03T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T14:52:53.000' AS DateTime), N'Admin', CAST(N'2018-11-02T08:21:54.500' AS DateTime), N'Admin')
INSERT [dbo].[Customer] ([Id], [Name], [Address], [Phone], [JoinDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'5bbff000-a2c5-44b2-a2c8-35d7538f4e6b', N'Lê Văn Sơn', N'478 Nguyễn Tất Thành, Đà Nẵng', N'0235123458', CAST(N'2018-11-01T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T14:54:21.140' AS DateTime), N'Admin', CAST(N'2018-10-30T14:54:21.140' AS DateTime), N'Admin')
INSERT [dbo].[Customer] ([Id], [Name], [Address], [Phone], [JoinDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'94bb5c87-48a2-46ac-bc3d-569bc7f2e88c', N'Trần Văn Hưng', N'43 Trần Cao Vân, Đà Nẵng', N'0978468782', CAST(N'2018-10-31T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T14:48:36.673' AS DateTime), N'Admin', CAST(N'2018-10-30T14:48:36.673' AS DateTime), N'Admin')
INSERT [dbo].[Customer] ([Id], [Name], [Address], [Phone], [JoinDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'd159f699-0139-473e-be96-5a766c882bc0', N'Nguyễn Hồng Duy', N'53 Nguyễn Tri Phương, Đà Nẵng', N'0935123457', CAST(N'2018-10-29T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T14:52:03.000' AS DateTime), N'Admin', CAST(N'2018-11-04T23:13:41.697' AS DateTime), N'Admin')
INSERT [dbo].[Customer] ([Id], [Name], [Address], [Phone], [JoinDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'56d15ccf-d0cb-48b6-818f-792475d050e0', N'Dư Bảo An', N'95 Hàm Nghi, Đà Nẵng', N'0234567953', CAST(N'2018-10-30T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T14:47:44.000' AS DateTime), N'Admin', CAST(N'2018-11-14T22:08:18.277' AS DateTime), N'Admin')
INSERT [dbo].[Customer] ([Id], [Name], [Address], [Phone], [JoinDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'50651405-87f8-477b-8dcb-b0bc14447a6e', N'Nguyễn Công Phượng', N'23 Hải Phòng, Đà Nẵng', N'0988944608', CAST(N'2018-10-30T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T14:50:02.550' AS DateTime), N'Admin', CAST(N'2018-10-30T14:50:02.550' AS DateTime), N'Admin')
INSERT [dbo].[Customer] ([Id], [Name], [Address], [Phone], [JoinDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'8a9a1470-fdc2-45af-b892-be1c0a5119b3', N'Vũ Văn Thanh', N'45 Điện Biên Phủ, Đà Nẵng', N'0954215789', CAST(N'2018-11-01T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T14:49:17.947' AS DateTime), N'Admin', CAST(N'2018-10-30T14:49:17.947' AS DateTime), N'Admin')
INSERT [dbo].[Customer] ([Id], [Name], [Address], [Phone], [JoinDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'73bb995e-c8b2-4469-8a26-c47806d3b2eb', N'Nguyễn Văn Toàn', N'23 Hai Bà Trưng, Quảng Nam', N'0905123456', CAST(N'2018-10-30T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T14:50:28.407' AS DateTime), N'Admin', CAST(N'2018-10-30T14:50:28.407' AS DateTime), N'Admin')
INSERT [dbo].[Customer] ([Id], [Name], [Address], [Phone], [JoinDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'b2364223-e09b-4395-9e8f-cc6348f52ecf', N'Trần Minh Vương', N'47 Trần Phú, Quảng Nam', N'0941380380', CAST(N'2018-10-31T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T14:50:59.627' AS DateTime), N'Admin', CAST(N'2018-10-30T14:50:59.627' AS DateTime), N'Admin')
INSERT [dbo].[Customer] ([Id], [Name], [Address], [Phone], [JoinDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'5972d493-4896-434e-ba39-cefe7f5a8662', N'Nguyễn Tuấn Anh', N'126 Nguyễn Lương Bằng, Đà Nẵng', N'0935421654', CAST(N'2018-11-02T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T14:53:44.207' AS DateTime), N'Admin', CAST(N'2018-10-30T14:53:44.207' AS DateTime), N'Admin')
INSERT [dbo].[Customer] ([Id], [Name], [Address], [Phone], [JoinDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'40bed182-0c26-43a1-a28e-db83fbc08dd8', N'Lục Xuân Hưng', N'37 Hàm Nghi, Đà Nẵng', N'0238794513', CAST(N'2018-11-10T00:00:00.000' AS DateTime), 0, CAST(N'2018-11-04T23:45:15.770' AS DateTime), N'Admin', CAST(N'2018-11-04T23:45:15.770' AS DateTime), N'Admin')
INSERT [dbo].[Delivery] ([Id], [SaleId], [Quantity], [DeliveriedDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'dc4a9fe1-ef4e-40a6-a451-0933545d6fec', N'9fc90c71-4688-403b-95bf-c2450e9b4113', 250, CAST(N'2018-11-05T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-31T10:41:06.000' AS DateTime), N'Admin', CAST(N'2018-11-02T08:22:39.137' AS DateTime), N'Admin')
INSERT [dbo].[Delivery] ([Id], [SaleId], [Quantity], [DeliveriedDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'bddc518e-2705-4612-b3ed-54a8cb2926e4', N'aa2a0d5d-6c3e-48bf-927f-6017408ce87a', 100, CAST(N'2018-11-20T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-31T10:40:29.203' AS DateTime), N'Admin', CAST(N'2018-10-31T10:40:29.203' AS DateTime), N'Admin')
INSERT [dbo].[Delivery] ([Id], [SaleId], [Quantity], [DeliveriedDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'07243438-a68e-4506-94c2-973cad475eff', N'94a408db-f670-43f9-8f67-b6b518863cf7', 200, CAST(N'2018-11-30T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-31T10:40:53.443' AS DateTime), N'Admin', CAST(N'2018-10-31T10:40:53.443' AS DateTime), N'Admin')
INSERT [dbo].[Delivery] ([Id], [SaleId], [Quantity], [DeliveriedDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'c4452212-d583-411a-9461-9f08c2bcbddd', N'54955d8a-7e8b-4ecf-af5a-6c4ef75b7078', 150, CAST(N'2018-10-30T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-31T10:40:40.547' AS DateTime), N'Admin', CAST(N'2018-10-31T10:40:40.547' AS DateTime), N'Admin')
INSERT [dbo].[Delivery] ([Id], [SaleId], [Quantity], [DeliveriedDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'678d9621-9b26-4039-9208-b39165b9fdef', N'd6d5c82a-8466-485a-9690-240c63ecf08a', 200, CAST(N'2018-11-10T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-31T10:38:46.643' AS DateTime), N'Admin', CAST(N'2018-10-31T10:38:46.647' AS DateTime), N'Admin')
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Phone], [JoinDate], [LeftDate], [Position], [Salary], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'0e7c0a65-7925-43b4-ac47-1f97992036a4', N'Nguyễn Anh Đức', N'ducna@donga.edu.vn', N'0978424242', CAST(N'2018-10-23T00:00:00.000' AS DateTime), NULL, N'Sale Executive ', 900.0000, 0, CAST(N'2018-10-30T15:21:24.000' AS DateTime), N'Admin', CAST(N'2018-11-02T08:22:02.260' AS DateTime), N'Admin')
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Phone], [JoinDate], [LeftDate], [Position], [Salary], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'd73be441-2ef4-472a-a6a9-23cb6ed6aaf4', N'Võ Ngọc Hiền', N'nhivnh@donga.edu.vn', N'0987547654', CAST(N'2018-10-20T00:00:00.000' AS DateTime), NULL, N'Sale Representtative', 900.0000, 0, CAST(N'2018-10-30T15:27:02.000' AS DateTime), N'Admin', CAST(N'2018-11-05T00:21:49.567' AS DateTime), N'Admin')
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Phone], [JoinDate], [LeftDate], [Position], [Salary], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'c65a2212-1d4b-4122-ac76-68384dd35b02', N'Bùi Tiến Dũng', N'dungbt@donga.edu.vn', N'0988456789', CAST(N'2018-10-25T00:00:00.000' AS DateTime), NULL, N'Sale Representtative ', 850.0000, 0, CAST(N'2018-10-30T15:22:16.333' AS DateTime), N'Admin', CAST(N'2018-10-30T15:22:16.333' AS DateTime), N'Admin')
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Phone], [JoinDate], [LeftDate], [Position], [Salary], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'46ad753d-659a-4340-818c-72690b52e1c7', N'Bùi Ngọc Vinh', N'vinhbn@donga.edu.vn', N'0237456789', CAST(N'2018-10-01T00:00:00.000' AS DateTime), NULL, N'Sale Supervisor', 1000.0000, 0, CAST(N'2018-10-30T15:27:45.000' AS DateTime), N'Admin', CAST(N'2018-11-14T15:53:14.620' AS DateTime), N'Admin')
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Phone], [JoinDate], [LeftDate], [Position], [Salary], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'3fcf30a4-b684-4961-95b5-b8b9ab48ce7d', N'Đỗ Duy Mạnh', N'manhdd@donga.edu.vn', N'0905414141', CAST(N'2018-10-25T00:00:00.000' AS DateTime), NULL, N'Sale Supervisor', 1000.0000, 0, CAST(N'2018-10-30T15:24:39.027' AS DateTime), N'Admin', CAST(N'2018-10-30T15:24:39.027' AS DateTime), N'Admin')
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Phone], [JoinDate], [LeftDate], [Position], [Salary], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'5d12d9aa-582e-4ab4-b85e-d8b469744580', N'Nguyễn Thị Ngọc Hiền', N'hienntn@donga.edu.vn', N'0935456789', CAST(N'2018-10-20T00:00:00.000' AS DateTime), NULL, N'Sale Executive', 800.0000, 0, CAST(N'2018-10-30T15:26:01.437' AS DateTime), N'Admin', CAST(N'2018-10-30T15:26:01.437' AS DateTime), N'Admin')
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Phone], [JoinDate], [LeftDate], [Position], [Salary], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'1a83ddbe-f8d2-4abc-84b0-ddfdebab4c08', N'Ngô Trần Tuấn Anh', N'anhntt@donga.edu.vn', N'0245789421', CAST(N'2018-10-20T00:00:00.000' AS DateTime), NULL, N'Sale Man', 700.0000, 0, CAST(N'2018-10-30T15:20:29.297' AS DateTime), N'Admin', CAST(N'2018-10-30T15:20:29.297' AS DateTime), N'Admin')
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Phone], [JoinDate], [LeftDate], [Position], [Salary], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'09337ec2-6611-447a-89e7-e836138a2a92', N'Trần Hữu Hùng', N'hungth@donga.edu.vn', N'0975421348', CAST(N'2018-10-05T00:00:00.000' AS DateTime), CAST(N'2019-02-28T00:00:00.000' AS DateTime), N'Sale Man', 700.0000, 0, CAST(N'2018-10-30T15:02:43.000' AS DateTime), N'Admin', CAST(N'2018-11-07T16:10:02.547' AS DateTime), N'Admin')
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Phone], [JoinDate], [LeftDate], [Position], [Salary], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'd38e339c-53a6-42f8-a297-eab9780874e1', N'Bùi Thế Toàn', N'toanbt@donga.edu.vn', N'0237894564', CAST(N'2018-10-10T00:00:00.000' AS DateTime), CAST(N'2019-01-30T00:00:00.000' AS DateTime), N'Sale Man', 800.0000, 0, CAST(N'2018-10-30T15:19:12.000' AS DateTime), N'Admin', CAST(N'2018-11-03T10:39:49.303' AS DateTime), N'Admin')
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Phone], [JoinDate], [LeftDate], [Position], [Salary], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'353540da-aa91-4ec8-bdec-f3c527599f96', N'Lê Văn Quang', N'quanglv@donga.edu.vn', N'0841345789', CAST(N'2018-10-01T00:00:00.000' AS DateTime), CAST(N'2019-05-30T00:00:00.000' AS DateTime), N'Product Management', 2000.0000, 0, CAST(N'2018-10-30T14:55:42.000' AS DateTime), N'Admin', CAST(N'2018-11-03T10:39:33.150' AS DateTime), N'Admin')
INSERT [dbo].[Item] ([Id], [Name], [Unit], [Amount], [DateExpired], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'bed7873e-c3a6-473c-bd06-217fd66e3ed6', N'Sữa chua Yomost', N'Bottle', 150.0000, CAST(N'2019-01-31T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T21:13:43.000' AS DateTime), N'Admin', CAST(N'2018-11-04T21:18:39.897' AS DateTime), N'Admin')
INSERT [dbo].[Item] ([Id], [Name], [Unit], [Amount], [DateExpired], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'0207af85-f9bf-4b28-b762-426c1c2cdcb3', N'Sữa tươi Vinamilk', N'Bottle', 100.0000, CAST(N'2018-12-31T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T21:12:53.000' AS DateTime), N'Admin', CAST(N'2018-11-02T14:26:52.893' AS DateTime), N'Admin')
INSERT [dbo].[Item] ([Id], [Name], [Unit], [Amount], [DateExpired], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'6304297c-9078-47f0-9c7c-66f29c3bd3b4', N'Sữa trẻ em Friso', N'Bottle', 200.0000, CAST(N'2019-01-31T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T21:14:36.000' AS DateTime), N'Admin', CAST(N'2018-11-02T14:26:58.180' AS DateTime), N'Admin')
INSERT [dbo].[Item] ([Id], [Name], [Unit], [Amount], [DateExpired], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'e0295cf4-0100-4b3a-9680-71dc8c51fa5d', N'Sữa người lớn Abbot', N'Can', 500.0000, CAST(N'2019-02-28T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T21:15:53.000' AS DateTime), N'Admin', CAST(N'2018-11-02T14:27:18.200' AS DateTime), N'Admin')
INSERT [dbo].[Item] ([Id], [Name], [Unit], [Amount], [DateExpired], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'bb4b733a-0dfe-46fa-92e5-9fa004f11a0f', N'Sữa bột Dutch Lady', N'Can', 300.0000, CAST(N'2019-01-31T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T21:16:43.000' AS DateTime), N'Admin', CAST(N'2018-11-14T09:31:03.480' AS DateTime), N'Admin')
INSERT [dbo].[Item] ([Id], [Name], [Unit], [Amount], [DateExpired], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'380e7957-1fbc-4118-8148-a6cccdd0c859', N'Sữa đặc ông thọ', N'Bottle', 350.0000, CAST(N'2019-02-21T00:00:00.000' AS DateTime), 0, CAST(N'2018-11-07T16:26:17.150' AS DateTime), N'Admin', CAST(N'2018-11-07T16:26:17.150' AS DateTime), N'Admin')
INSERT [dbo].[MenuItem] ([Id], [ItemId], [CreateDate], [LastUsedDate], [Rating], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'911c2e53-a165-43de-b631-2b8ab82f6eb6', N'bed7873e-c3a6-473c-bd06-217fd66e3ed6', CAST(N'2018-10-01T00:00:00.000' AS DateTime), CAST(N'2018-12-31T00:00:00.000' AS DateTime), 5, 0, CAST(N'2018-10-30T21:17:23.000' AS DateTime), N'Admin', CAST(N'2018-11-02T08:22:17.580' AS DateTime), N'Admin')
INSERT [dbo].[MenuItem] ([Id], [ItemId], [CreateDate], [LastUsedDate], [Rating], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'9639ecf4-7dfa-46c6-af48-905091c3b803', N'bb4b733a-0dfe-46fa-92e5-9fa004f11a0f', CAST(N'2018-10-30T00:00:00.000' AS DateTime), CAST(N'2019-03-31T00:00:00.000' AS DateTime), 4, 0, CAST(N'2018-10-30T21:17:59.000' AS DateTime), N'Admin', CAST(N'2018-11-06T22:34:03.547' AS DateTime), N'Admin')
INSERT [dbo].[MenuItem] ([Id], [ItemId], [CreateDate], [LastUsedDate], [Rating], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'edab3e92-3785-4e62-bc9a-934825f4bcb0', N'0207af85-f9bf-4b28-b762-426c1c2cdcb3', CAST(N'2018-10-10T00:00:00.000' AS DateTime), CAST(N'2018-12-31T00:00:00.000' AS DateTime), 5, 0, CAST(N'2018-10-30T21:17:37.200' AS DateTime), N'Admin', CAST(N'2018-10-30T21:17:37.200' AS DateTime), N'Admin')
INSERT [dbo].[MenuItem] ([Id], [ItemId], [CreateDate], [LastUsedDate], [Rating], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'7f15d874-ad35-49c0-990a-a6077f2b7ba2', N'e0295cf4-0100-4b3a-9680-71dc8c51fa5d', CAST(N'2018-10-20T00:00:00.000' AS DateTime), CAST(N'2019-01-30T00:00:00.000' AS DateTime), 4, 0, CAST(N'2018-10-30T21:18:25.000' AS DateTime), N'Admin', CAST(N'2018-11-05T12:29:51.810' AS DateTime), N'Admin')
INSERT [dbo].[MenuItem] ([Id], [ItemId], [CreateDate], [LastUsedDate], [Rating], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'0dd72df4-baa9-4922-a9c1-c8d8afa382bc', N'6304297c-9078-47f0-9c7c-66f29c3bd3b4', CAST(N'2018-10-01T00:00:00.000' AS DateTime), CAST(N'2019-01-31T00:00:00.000' AS DateTime), 5, 0, CAST(N'2018-10-30T21:17:47.217' AS DateTime), N'Admin', CAST(N'2018-10-30T21:17:47.217' AS DateTime), N'Admin')
INSERT [dbo].[PickUp] ([Id], [SupplierId], [EmployeeId], [PickUpDate], [Price], [Quantity], [Address], [Unit], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'e955d974-af71-4a89-bf9e-3c1a33b683c0', N'89205bff-82a9-4503-8c1f-822f00678f89', N'c65a2212-1d4b-4122-ac76-68384dd35b02', CAST(N'2018-10-05T00:00:00.000' AS DateTime), 1.5900, 200, N'Đà Nẵng', N'Box', 0, CAST(N'2018-10-30T16:59:47.000' AS DateTime), N'Admin', CAST(N'2018-11-14T22:24:09.683' AS DateTime), N'Admin')
INSERT [dbo].[PickUp] ([Id], [SupplierId], [EmployeeId], [PickUpDate], [Price], [Quantity], [Address], [Unit], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'1d4ebd08-be2d-442c-8423-711f3d5b480d', N'4396581d-4047-4b41-ad5e-f07b49c177c2', N'09337ec2-6611-447a-89e7-e836138a2a92', CAST(N'2018-10-25T00:00:00.000' AS DateTime), 0.9900, 1000, N'Huế', N'Box', 0, CAST(N'2018-10-30T21:04:11.000' AS DateTime), N'Admin', CAST(N'2018-11-02T14:11:04.487' AS DateTime), N'Admin')
INSERT [dbo].[PickUp] ([Id], [SupplierId], [EmployeeId], [PickUpDate], [Price], [Quantity], [Address], [Unit], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'41b4c527-5036-416c-a7e2-c92c72706e9f', N'3895e77d-b63c-4840-a287-c7d1cdb9c44a', N'd38e339c-53a6-42f8-a297-eab9780874e1', CAST(N'2018-11-18T00:00:00.000' AS DateTime), 0.9900, 200, N'Quãng Ngãi', N'Bottle', 0, CAST(N'2018-11-17T14:17:46.120' AS DateTime), N'Admin', CAST(N'2018-11-17T14:17:46.123' AS DateTime), N'Admin')
INSERT [dbo].[PickUp] ([Id], [SupplierId], [EmployeeId], [PickUpDate], [Price], [Quantity], [Address], [Unit], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'495ba97e-35d3-4950-8fa1-db3e66801bb1', N'c0b19884-0a14-481b-aeaf-63b556623b7b', N'0e7c0a65-7925-43b4-ac47-1f97992036a4', CAST(N'2018-10-01T00:00:00.000' AS DateTime), 1.9900, 100, N'Đà Nẵng', N'Box', 0, CAST(N'2018-10-30T21:01:59.000' AS DateTime), N'Admin', CAST(N'2018-11-02T14:11:07.880' AS DateTime), N'Admin')
INSERT [dbo].[PickUp] ([Id], [SupplierId], [EmployeeId], [PickUpDate], [Price], [Quantity], [Address], [Unit], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'bf685a3e-6f88-4443-8cf6-ebf83c606bf4', N'66893334-6aae-4786-8c5a-c385cd1c6b2d', N'3fcf30a4-b684-4961-95b5-b8b9ab48ce7d', CAST(N'2018-10-10T00:00:00.000' AS DateTime), 1.9900, 150, N'Quảng Nam', N'Box', 0, CAST(N'2018-10-30T21:03:24.000' AS DateTime), N'Admin', CAST(N'2018-11-02T14:11:11.477' AS DateTime), N'Admin')
INSERT [dbo].[Property] ([Id], [ItemId], [Description], [Status], [OwnedDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'23f55bee-0d91-4ac4-a2fc-03f390ff6382', N'0207af85-f9bf-4b28-b762-426c1c2cdcb3', NULL, N'Còn hàng', CAST(N'2018-10-05T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T21:21:43.000' AS DateTime), N'Admin', CAST(N'2018-11-05T10:45:20.660' AS DateTime), N'Admin')
INSERT [dbo].[Property] ([Id], [ItemId], [Description], [Status], [OwnedDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'4009b2cc-5708-4a8a-994e-3a6bc4f60802', N'6304297c-9078-47f0-9c7c-66f29c3bd3b4', NULL, N'Còn hàng', CAST(N'2018-10-30T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T21:24:43.000' AS DateTime), N'Admin', CAST(N'2018-10-30T21:24:43.000' AS DateTime), N'Admin')
INSERT [dbo].[Property] ([Id], [ItemId], [Description], [Status], [OwnedDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'5ce676e8-92f7-4334-a7cc-41909a40ab1c', N'bb4b733a-0dfe-46fa-92e5-9fa004f11a0f', NULL, N'Còn hàng', CAST(N'2018-11-15T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T21:25:06.000' AS DateTime), N'Admin', CAST(N'2018-11-06T22:34:19.897' AS DateTime), N'Admin')
INSERT [dbo].[Property] ([Id], [ItemId], [Description], [Status], [OwnedDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'5bd83d61-668c-45a5-8c8b-7bbcf52864c2', N'e0295cf4-0100-4b3a-9680-71dc8c51fa5d', NULL, N'Tạm hết hàng', CAST(N'2018-11-30T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T21:24:56.000' AS DateTime), N'Admin', CAST(N'2018-11-05T10:45:29.387' AS DateTime), N'Admin')
INSERT [dbo].[Property] ([Id], [ItemId], [Description], [Status], [OwnedDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'abab5637-2289-4385-bac3-bb0a3c4e36a5', N'bed7873e-c3a6-473c-bd06-217fd66e3ed6', NULL, N'Tạm hết hàng', CAST(N'2018-11-20T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T21:22:11.000' AS DateTime), N'Admin', CAST(N'2018-11-05T10:41:33.943' AS DateTime), N'Admin')
INSERT [dbo].[Purchase] ([Id], [SupplierId], [Description], [Price], [PurchaseDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'98637ee3-5512-44d6-adeb-2aa85ab4c6ca', N'89205bff-82a9-4503-8c1f-822f00678f89', N'Sữa sạch, thức uống sạch', 1.5900, CAST(N'2018-10-05T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T15:48:38.000' AS DateTime), N'Admin', CAST(N'2018-11-02T08:22:34.173' AS DateTime), N'Admin')
INSERT [dbo].[Purchase] ([Id], [SupplierId], [Description], [Price], [PurchaseDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'390417e0-9d2a-4eb0-bf22-3678e0439e1e', N'493b9858-6c7b-43a3-a9f7-9f9e31c353ed', N'Sữa tươi tiệt trùng', 1.5900, CAST(N'2018-10-05T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T15:49:40.923' AS DateTime), N'Admin', CAST(N'2018-10-30T15:49:40.923' AS DateTime), N'Admin')
INSERT [dbo].[Purchase] ([Id], [SupplierId], [Description], [Price], [PurchaseDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'7364d2c0-c61e-4ddd-8005-6206aac68a26', N'2af8d094-7bfb-4215-9f10-d7433a34af80', N'Sữa nguyên chất cho trẻ em. Giúp tăng chiều cao', 1.9900, CAST(N'2018-10-10T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T15:52:23.553' AS DateTime), N'Admin', CAST(N'2018-10-30T15:52:23.553' AS DateTime), N'Admin')
INSERT [dbo].[Purchase] ([Id], [SupplierId], [Description], [Price], [PurchaseDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'8259ce62-6e7e-40da-bca1-6bfa2db7598c', N'0482ea10-8302-4bee-a453-de98a53a615c', N'Thức uống vị sữa chua', 1.1900, CAST(N'2018-10-20T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T15:53:15.207' AS DateTime), N'Admin', CAST(N'2018-10-30T15:53:15.207' AS DateTime), N'Admin')
INSERT [dbo].[Purchase] ([Id], [SupplierId], [Description], [Price], [PurchaseDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'9ff4dc73-dc63-4b29-9840-9b74319ed9bb', N'c0b19884-0a14-481b-aeaf-63b556623b7b', N'Sữa đậu nành nguyên chất', 0.9900, CAST(N'2018-10-01T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T15:39:46.000' AS DateTime), N'Admin', CAST(N'2018-10-30T15:39:46.000' AS DateTime), N'Admin')
INSERT [dbo].[Purchase] ([Id], [SupplierId], [Description], [Price], [PurchaseDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'28fd7831-573d-438f-ab7f-d31ec3ae1dbe', N'bad3a75d-4b62-471c-8513-e632bdb0d27d', N'Sữa cho người lớn, xuất khẩu từ Hoa Kỳ', 1.9900, CAST(N'2018-10-25T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T15:53:50.000' AS DateTime), N'Admin', CAST(N'2018-11-06T22:34:27.290' AS DateTime), N'Admin')
INSERT [dbo].[Purchase] ([Id], [SupplierId], [Description], [Price], [PurchaseDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'27627081-4f44-4dc9-9e5c-d9f99d1715c4', N'3895e77d-b63c-4840-a287-c7d1cdb9c44a', N'Sữa tiệt trùng có đường và không đường', 1.5900, CAST(N'2018-10-10T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T15:51:38.317' AS DateTime), N'Admin', CAST(N'2018-10-30T15:51:38.317' AS DateTime), N'Admin')
INSERT [dbo].[Purchase] ([Id], [SupplierId], [Description], [Price], [PurchaseDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'28466062-fa96-4601-949a-e49bbb4c6810', N'4396581d-4047-4b41-ad5e-f07b49c177c2', N'Sữa tươi sạch, tiệt trùng', 1.5900, CAST(N'2018-10-30T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T15:54:23.247' AS DateTime), N'Admin', CAST(N'2018-10-30T15:54:23.247' AS DateTime), N'Admin')
INSERT [dbo].[Purchase] ([Id], [SupplierId], [Description], [Price], [PurchaseDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'064a487e-983c-4867-a1ff-e5f0a1ed70ec', N'66893334-6aae-4786-8c5a-c385cd1c6b2d', N'Sữa tươi tiệt trùng', 0.9900, CAST(N'2018-10-01T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T15:50:39.987' AS DateTime), N'Admin', CAST(N'2018-10-30T15:50:39.987' AS DateTime), N'Admin')
INSERT [dbo].[Sale] ([Id], [EmployeeId], [CustomerId], [SaleCode], [Unit], [Price], [ItemId], [Quantity], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'd6d5c82a-8466-485a-9690-240c63ecf08a', N'0e7c0a65-7925-43b4-ac47-1f97992036a4', N'1ceef734-8fb9-4a0c-be01-14e2cb8acbc8', N'GroupOne-11022018-1406', N'Box', 1.5900, N'bed7873e-c3a6-473c-bd06-217fd66e3ed6', 100, 0, CAST(N'2018-10-31T10:02:46.000' AS DateTime), N'Admin', CAST(N'2018-11-02T14:16:17.017' AS DateTime), N'Admin')
INSERT [dbo].[Sale] ([Id], [EmployeeId], [CustomerId], [SaleCode], [Unit], [Price], [ItemId], [Quantity], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'aa2a0d5d-6c3e-48bf-927f-6017408ce87a', N'c65a2212-1d4b-4122-ac76-68384dd35b02', N'94bb5c87-48a2-46ac-bc3d-569bc7f2e88c', N'GroupOne-11062018-2233', N'Box', 1.9900, N'0207af85-f9bf-4b28-b762-426c1c2cdcb3', 200, 0, CAST(N'2018-10-31T10:03:11.000' AS DateTime), N'Admin', CAST(N'2018-11-06T22:33:33.833' AS DateTime), N'Admin')
INSERT [dbo].[Sale] ([Id], [EmployeeId], [CustomerId], [SaleCode], [Unit], [Price], [ItemId], [Quantity], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'54955d8a-7e8b-4ecf-af5a-6c4ef75b7078', N'5d12d9aa-582e-4ab4-b85e-d8b469744580', N'50651405-87f8-477b-8dcb-b0bc14447a6e', N'GroupOne-10312018-1027', N'Box', 2.5900, N'6304297c-9078-47f0-9c7c-66f29c3bd3b4', 500, 0, CAST(N'2018-10-31T10:27:51.000' AS DateTime), N'Admin', CAST(N'2018-11-02T14:06:54.653' AS DateTime), N'Admin')
INSERT [dbo].[Sale] ([Id], [EmployeeId], [CustomerId], [SaleCode], [Unit], [Price], [ItemId], [Quantity], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'94a408db-f670-43f9-8f67-b6b518863cf7', N'5d12d9aa-582e-4ab4-b85e-d8b469744580', N'94bb5c87-48a2-46ac-bc3d-569bc7f2e88c', N'GroupOne-10312018-1028', N'Box', 1.2900, N'e0295cf4-0100-4b3a-9680-71dc8c51fa5d', 500, 0, CAST(N'2018-10-31T10:28:31.000' AS DateTime), N'Admin', CAST(N'2018-11-02T14:07:06.353' AS DateTime), N'Admin')
INSERT [dbo].[Sale] ([Id], [EmployeeId], [CustomerId], [SaleCode], [Unit], [Price], [ItemId], [Quantity], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'55bd23a2-667c-4f22-9832-bfd7d546b256', N'd38e339c-53a6-42f8-a297-eab9780874e1', N'94bb5c87-48a2-46ac-bc3d-569bc7f2e88c', N'GroupOne-11172018-1410', N'Bottle', 1.1900, N'e0295cf4-0100-4b3a-9680-71dc8c51fa5d', 120, 0, CAST(N'2018-11-17T14:10:16.000' AS DateTime), N'Admin', CAST(N'2018-11-17T14:10:29.417' AS DateTime), N'Admin')
INSERT [dbo].[Sale] ([Id], [EmployeeId], [CustomerId], [SaleCode], [Unit], [Price], [ItemId], [Quantity], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'9fc90c71-4688-403b-95bf-c2450e9b4113', N'd38e339c-53a6-42f8-a297-eab9780874e1', N'8a9a1470-fdc2-45af-b892-be1c0a5119b3', N'GroupOne-11142018-1105', N'Box', 1.6900, N'bb4b733a-0dfe-46fa-92e5-9fa004f11a0f', 400, 0, CAST(N'2018-10-31T10:26:56.000' AS DateTime), N'Admin', CAST(N'2018-11-14T11:05:06.700' AS DateTime), N'Admin')
INSERT [dbo].[Supplier] ([Id], [Name], [Rating], [ContactNo], [ProvideDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'c0b19884-0a14-481b-aeaf-63b556623b7b', N'Vinasoy', 4, N'0236454545', CAST(N'2018-10-20T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T15:34:19.000' AS DateTime), N'Admin', CAST(N'2018-11-02T08:22:06.237' AS DateTime), N'Admin')
INSERT [dbo].[Supplier] ([Id], [Name], [Rating], [ContactNo], [ProvideDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'89205bff-82a9-4503-8c1f-822f00678f89', N'Nuti Food', 5, N'0239454545', CAST(N'2018-10-15T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T15:37:35.000' AS DateTime), N'Admin', CAST(N'2018-10-30T15:37:35.000' AS DateTime), N'Admin')
INSERT [dbo].[Supplier] ([Id], [Name], [Rating], [ContactNo], [ProvideDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'493b9858-6c7b-43a3-a9f7-9f9e31c353ed', N'Dutch Lady', 3, N'0235789789', CAST(N'2018-10-10T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T15:36:32.670' AS DateTime), N'Admin', CAST(N'2018-10-30T15:36:32.670' AS DateTime), N'Admin')
INSERT [dbo].[Supplier] ([Id], [Name], [Rating], [ContactNo], [ProvideDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'66893334-6aae-4786-8c5a-c385cd1c6b2d', N'Vinamilk', 5, N'0236373737', CAST(N'2018-10-01T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T15:33:03.960' AS DateTime), N'Admin', CAST(N'2018-10-30T15:33:03.960' AS DateTime), N'Admin')
INSERT [dbo].[Supplier] ([Id], [Name], [Rating], [ContactNo], [ProvideDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'3895e77d-b63c-4840-a287-c7d1cdb9c44a', N'Cô Gái Hà Lan', 5, N'0236123123', CAST(N'2018-10-15T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T15:35:07.787' AS DateTime), N'Admin', CAST(N'2018-10-30T15:35:07.787' AS DateTime), N'Admin')
INSERT [dbo].[Supplier] ([Id], [Name], [Rating], [ContactNo], [ProvideDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'2af8d094-7bfb-4215-9f10-d7433a34af80', N'Friso', 5, N'0236454647', CAST(N'2018-10-05T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T15:36:08.087' AS DateTime), N'Admin', CAST(N'2018-10-30T15:36:08.087' AS DateTime), N'Admin')
INSERT [dbo].[Supplier] ([Id], [Name], [Rating], [ContactNo], [ProvideDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'0482ea10-8302-4bee-a453-de98a53a615c', N'Yomost', 4, N'0235744774', CAST(N'2018-10-15T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T15:37:04.933' AS DateTime), N'Admin', CAST(N'2018-10-30T15:37:04.933' AS DateTime), N'Admin')
INSERT [dbo].[Supplier] ([Id], [Name], [Rating], [ContactNo], [ProvideDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'bad3a75d-4b62-471c-8513-e632bdb0d27d', N'Abbot', 4, N'0233456456', CAST(N'2018-10-30T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T15:38:24.000' AS DateTime), N'Admin', CAST(N'2018-11-14T16:06:42.140' AS DateTime), N'Admin')
INSERT [dbo].[Supplier] ([Id], [Name], [Rating], [ContactNo], [ProvideDate], [IsDeleted], [InsAt], [InsBy], [UpdAt], [UpdBy]) VALUES (N'4396581d-4047-4b41-ad5e-f07b49c177c2', N'TH TrueMilk', 4, N'0236989898', CAST(N'2018-10-10T00:00:00.000' AS DateTime), 0, CAST(N'2018-10-30T15:33:32.357' AS DateTime), N'Admin', CAST(N'2018-10-30T15:33:32.357' AS DateTime), N'Admin')
ALTER TABLE [dbo].[Delivery]  WITH CHECK ADD FOREIGN KEY([SaleId])
REFERENCES [dbo].[Sale] ([Id])
GO
ALTER TABLE [dbo].[Delivery]  WITH CHECK ADD FOREIGN KEY([SaleId])
REFERENCES [dbo].[Sale] ([Id])
GO
ALTER TABLE [dbo].[Delivery]  WITH CHECK ADD FOREIGN KEY([SaleId])
REFERENCES [dbo].[Sale] ([Id])
GO
ALTER TABLE [dbo].[Delivery]  WITH CHECK ADD FOREIGN KEY([SaleId])
REFERENCES [dbo].[Sale] ([Id])
GO
ALTER TABLE [dbo].[MenuItem]  WITH CHECK ADD FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([Id])
GO
ALTER TABLE [dbo].[MenuItem]  WITH CHECK ADD FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([Id])
GO
ALTER TABLE [dbo].[MenuItem]  WITH CHECK ADD FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([Id])
GO
ALTER TABLE [dbo].[MenuItem]  WITH CHECK ADD FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([Id])
GO
ALTER TABLE [dbo].[PickUp]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[PickUp]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[PickUp]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[PickUp]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[PickUp]  WITH CHECK ADD FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([Id])
GO
ALTER TABLE [dbo].[PickUp]  WITH CHECK ADD FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([Id])
GO
ALTER TABLE [dbo].[PickUp]  WITH CHECK ADD FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([Id])
GO
ALTER TABLE [dbo].[PickUp]  WITH CHECK ADD FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([Id])
GO
ALTER TABLE [dbo].[Property]  WITH CHECK ADD FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([Id])
GO
ALTER TABLE [dbo].[Property]  WITH CHECK ADD FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([Id])
GO
ALTER TABLE [dbo].[Property]  WITH CHECK ADD FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([Id])
GO
ALTER TABLE [dbo].[Property]  WITH CHECK ADD FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([Id])
GO
ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([Id])
GO
ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([Id])
GO
ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([Id])
GO
ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([Id])
GO
ALTER TABLE [dbo].[Sale]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Sale]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Sale]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Sale]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Sale]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Sale]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Sale]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Sale]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Sale]  WITH CHECK ADD FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([Id])
GO
ALTER TABLE [dbo].[Sale]  WITH CHECK ADD FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([Id])
GO
ALTER TABLE [dbo].[Sale]  WITH CHECK ADD FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([Id])
GO
ALTER TABLE [dbo].[Sale]  WITH CHECK ADD FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([Id])
GO
USE [master]
GO
ALTER DATABASE [SaleManagementSystem] SET  READ_WRITE 
GO
