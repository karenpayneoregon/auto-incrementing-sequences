
USE [NextValueDatabase]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 4/22/2025 6:59:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerIdentifier] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](40) NOT NULL,
	[ContactName] [nvarchar](30) NULL,
	[ContactTitle] [nvarchar](30) NULL,
	[City] [nvarchar](15) NULL,
	[PostalCode] [nvarchar](10) NULL,
	[Country] [nvarchar](15) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerIdentifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerSequence]    Script Date: 4/22/2025 6:59:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerSequence](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerIdentifier] [int] NULL,
	[CurrentSequenceValue] [nvarchar](max) NULL,
	[SequencePreFix] [nchar](2) NULL,
	[Sequence]  AS ([SequencePreFix]+[CurrentSequenceValue]),
 CONSTRAINT [PK_CustomerSequence] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Example]    Script Date: 4/22/2025 6:59:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Example](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_Example] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Example1]    Script Date: 4/22/2025 6:59:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Example1](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceNumber] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Example1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 4/22/2025 6:59:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerIdentifier] [int] NULL,
	[InvoiceNumber] [nvarchar](max) NULL,
	[OrderDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SampleComputed]    Script Date: 4/22/2025 6:59:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SampleComputed](
	[Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[ComputedIdentifier]  AS (case when [Id]<(9999) then 'A'+right('000'+CONVERT([varchar],[Id],(10)),(4)) else 'A'+CONVERT([varchar],[Id],(10)) end) PERSISTED,
	[CustomerName] [nvarchar](50) NULL,
 CONSTRAINT [PK_SampleComputed] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SampleComputed1]    Script Date: 4/22/2025 6:59:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SampleComputed1](
	[Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[ComputedIdentifier]  AS (case when [Id]<(9999) then 'A'+right('000'+CONVERT([varchar],[Id],(10)),(4)) else 'A'+CONVERT([varchar],[Id],(10)) end) PERSISTED,
	[CustomerIdentifer] [int] NULL,
 CONSTRAINT [PK_SampleComputed1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([CustomerIdentifier], [CompanyName], [ContactName], [ContactTitle], [City], [PostalCode], [Country]) VALUES (1, N'Alfreds Futterkiste', N'Maria Anders', N'Sales Representative', N'Berlin', N'12209', N'Germany')
INSERT [dbo].[Customers] ([CustomerIdentifier], [CompanyName], [ContactName], [ContactTitle], [City], [PostalCode], [Country]) VALUES (2, N'Ana Trujillo Emparedados y helados', N'Ana Trujillo', N'Owner', N'México D.F.', N'05021', N'Mexico')
INSERT [dbo].[Customers] ([CustomerIdentifier], [CompanyName], [ContactName], [ContactTitle], [City], [PostalCode], [Country]) VALUES (3, N'Antonio Moreno Taquería', N'Antonio Moreno', N'Owner', N'México D.F.', N'05023', N'Mexico')
INSERT [dbo].[Customers] ([CustomerIdentifier], [CompanyName], [ContactName], [ContactTitle], [City], [PostalCode], [Country]) VALUES (4, N'Around the Horn', N'Thomas Hardy', N'Sales Representative', N'London', N'WA1 1DP', N'UK')
INSERT [dbo].[Customers] ([CustomerIdentifier], [CompanyName], [ContactName], [ContactTitle], [City], [PostalCode], [Country]) VALUES (5, N'Berglunds snabbköp', N'Christina Berglund', N'Order Administrator', N'Luleå', N'S-958 22', N'Sweden')
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[CustomerSequence] ON 

INSERT [dbo].[CustomerSequence] ([Id], [CustomerIdentifier], [CurrentSequenceValue], [SequencePreFix]) VALUES (1, 1, NULL, N'AA')
INSERT [dbo].[CustomerSequence] ([Id], [CustomerIdentifier], [CurrentSequenceValue], [SequencePreFix]) VALUES (2, 2, N'000003', N'BB')
INSERT [dbo].[CustomerSequence] ([Id], [CustomerIdentifier], [CurrentSequenceValue], [SequencePreFix]) VALUES (3, 3, NULL, N'CC')
INSERT [dbo].[CustomerSequence] ([Id], [CustomerIdentifier], [CurrentSequenceValue], [SequencePreFix]) VALUES (4, 4, NULL, N'DD')
INSERT [dbo].[CustomerSequence] ([Id], [CustomerIdentifier], [CurrentSequenceValue], [SequencePreFix]) VALUES (5, 5, NULL, N'EE')
SET IDENTITY_INSERT [dbo].[CustomerSequence] OFF
GO
SET IDENTITY_INSERT [dbo].[Example1] ON 

INSERT [dbo].[Example1] ([Id], [InvoiceNumber]) VALUES (1, N'A001/20')
INSERT [dbo].[Example1] ([Id], [InvoiceNumber]) VALUES (2, N'A002/20')
INSERT [dbo].[Example1] ([Id], [InvoiceNumber]) VALUES (3, N'A003/20')
INSERT [dbo].[Example1] ([Id], [InvoiceNumber]) VALUES (4, N'A004/20')
INSERT [dbo].[Example1] ([Id], [InvoiceNumber]) VALUES (5, N'A005/20')
INSERT [dbo].[Example1] ([Id], [InvoiceNumber]) VALUES (6, N'A006/20')
INSERT [dbo].[Example1] ([Id], [InvoiceNumber]) VALUES (7, N'A007/20')
INSERT [dbo].[Example1] ([Id], [InvoiceNumber]) VALUES (8, N'A008/20')
INSERT [dbo].[Example1] ([Id], [InvoiceNumber]) VALUES (9, N'A009/20')
INSERT [dbo].[Example1] ([Id], [InvoiceNumber]) VALUES (10, N'A010/20')
INSERT [dbo].[Example1] ([Id], [InvoiceNumber]) VALUES (11, N'A011/20')
INSERT [dbo].[Example1] ([Id], [InvoiceNumber]) VALUES (12, N'A012/20')
INSERT [dbo].[Example1] ([Id], [InvoiceNumber]) VALUES (13, N'A013/20')
INSERT [dbo].[Example1] ([Id], [InvoiceNumber]) VALUES (14, N'A014/20')
INSERT [dbo].[Example1] ([Id], [InvoiceNumber]) VALUES (15, N'A015/20')
INSERT [dbo].[Example1] ([Id], [InvoiceNumber]) VALUES (16, N'A016/20')
INSERT [dbo].[Example1] ([Id], [InvoiceNumber]) VALUES (17, N'A017/20')
INSERT [dbo].[Example1] ([Id], [InvoiceNumber]) VALUES (18, N'A018/20')
INSERT [dbo].[Example1] ([Id], [InvoiceNumber]) VALUES (19, N'A019/20')
INSERT [dbo].[Example1] ([Id], [InvoiceNumber]) VALUES (20, N'A020/20')
SET IDENTITY_INSERT [dbo].[Example1] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OrderId], [CustomerIdentifier], [InvoiceNumber], [OrderDate]) VALUES (1, 3, N'CC1', CAST(N'2023-06-14T06:24:33.2373720' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[SampleComputed] ON 

INSERT [dbo].[SampleComputed] ([Id], [CustomerName]) VALUES (CAST(1 AS Numeric(18, 0)), N'1')
INSERT [dbo].[SampleComputed] ([Id], [CustomerName]) VALUES (CAST(2 AS Numeric(18, 0)), N'2')
SET IDENTITY_INSERT [dbo].[SampleComputed] OFF
GO
SET IDENTITY_INSERT [dbo].[SampleComputed1] ON 

INSERT [dbo].[SampleComputed1] ([Id], [CustomerIdentifer]) VALUES (CAST(1 AS Numeric(18, 0)), 1)
INSERT [dbo].[SampleComputed1] ([Id], [CustomerIdentifer]) VALUES (CAST(2 AS Numeric(18, 0)), 2)
INSERT [dbo].[SampleComputed1] ([Id], [CustomerIdentifer]) VALUES (CAST(3 AS Numeric(18, 0)), 3)
INSERT [dbo].[SampleComputed1] ([Id], [CustomerIdentifer]) VALUES (CAST(4 AS Numeric(18, 0)), 4)
SET IDENTITY_INSERT [dbo].[SampleComputed1] OFF
GO
ALTER TABLE [dbo].[CustomerSequence]  WITH CHECK ADD  CONSTRAINT [FK_CustomerSequence_Customers] FOREIGN KEY([CustomerIdentifier])
REFERENCES [dbo].[Customers] ([CustomerIdentifier])
GO
ALTER TABLE [dbo].[CustomerSequence] CHECK CONSTRAINT [FK_CustomerSequence_Customers]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Customers] FOREIGN KEY([CustomerIdentifier])
REFERENCES [dbo].[Customers] ([CustomerIdentifier])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Customers]
GO
USE [master]
GO
ALTER DATABASE [NextValueDatabase] SET  READ_WRITE 
GO
