USE [KonfigKc]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 29.11.2023 19:27:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[DepartmentID] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Departme__B2079BCD5DCFE8D3] PRIMARY KEY CLUSTERED 
(
	[DepartmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Files]    Script Date: 29.11.2023 19:27:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Files](
	[FileID] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](255) NOT NULL,
	[FileContent] [varbinary](max) NOT NULL,
 CONSTRAINT [PK__Files__6F0F989F7A38FB5B] PRIMARY KEY CLUSTERED 
(
	[FileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LicensiaInfo]    Script Date: 29.11.2023 19:27:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LicensiaInfo](
	[LicenseID] [int] IDENTITY(1,1) NOT NULL,
	[LicenseInfo] [nvarchar](50) NULL,
 CONSTRAINT [PK_LicensiaInfo] PRIMARY KEY CLUSTERED 
(
	[LicenseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Positions]    Script Date: 29.11.2023 19:27:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Positions](
	[PositionID] [int] IDENTITY(1,1) NOT NULL,
	[PositionName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK__Position__60BB9A59E986E9B5] PRIMARY KEY CLUSTERED 
(
	[PositionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Requests]    Script Date: 29.11.2023 19:27:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Requests](
	[RequestID] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentID] [int] NULL,
	[PositionID] [int] NULL,
	[RequestDate] [datetime] NULL,
	[StatusID] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK__Requests__33A8519ABBE2118D] PRIMARY KEY CLUSTERED 
(
	[RequestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Software]    Script Date: 29.11.2023 19:27:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Software](
	[SoftwareID] [int] IDENTITY(1,1) NOT NULL,
	[SoftwareName] [nvarchar](50) NOT NULL,
	[LicenseExpirationDate] [date] NULL,
	[Version] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[WebUrl] [nvarchar](255) NULL,
	[LastUpdateDate] [date] NULL,
	[UpdateDescription] [nvarchar](max) NULL,
	[FileID] [int] NULL,
 CONSTRAINT [PK__Software__25EDB8DC2579DCD7] PRIMARY KEY CLUSTERED 
(
	[SoftwareID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SoftwarePosition]    Script Date: 29.11.2023 19:27:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SoftwarePosition](
	[SoftwarePositionID] [int] IDENTITY(1,1) NOT NULL,
	[PositionID] [int] NOT NULL,
	[SoftwareID] [int] NOT NULL,
	[LicenseID] [int] NOT NULL,
 CONSTRAINT [PK_SoftwarePosition] PRIMARY KEY CLUSTERED 
(
	[SoftwarePositionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 29.11.2023 19:27:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[StatusID] [int] IDENTITY(1,1) NOT NULL,
	[NameStatus] [nvarchar](50) NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD  CONSTRAINT [FK_Requests_Departments] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Departments] ([DepartmentID])
GO
ALTER TABLE [dbo].[Requests] CHECK CONSTRAINT [FK_Requests_Departments]
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD  CONSTRAINT [FK_Requests_Positions] FOREIGN KEY([PositionID])
REFERENCES [dbo].[Positions] ([PositionID])
GO
ALTER TABLE [dbo].[Requests] CHECK CONSTRAINT [FK_Requests_Positions]
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD  CONSTRAINT [FK_Requests_Status1] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Status] ([StatusID])
GO
ALTER TABLE [dbo].[Requests] CHECK CONSTRAINT [FK_Requests_Status1]
GO
ALTER TABLE [dbo].[Software]  WITH CHECK ADD  CONSTRAINT [FK__Software__FileID__2B3F6F97] FOREIGN KEY([FileID])
REFERENCES [dbo].[Files] ([FileID])
GO
ALTER TABLE [dbo].[Software] CHECK CONSTRAINT [FK__Software__FileID__2B3F6F97]
GO
ALTER TABLE [dbo].[SoftwarePosition]  WITH CHECK ADD  CONSTRAINT [FK_SoftwarePosition_LicensiaInfo1] FOREIGN KEY([LicenseID])
REFERENCES [dbo].[LicensiaInfo] ([LicenseID])
GO
ALTER TABLE [dbo].[SoftwarePosition] CHECK CONSTRAINT [FK_SoftwarePosition_LicensiaInfo1]
GO
ALTER TABLE [dbo].[SoftwarePosition]  WITH CHECK ADD  CONSTRAINT [FK_SoftwarePosition_Positions] FOREIGN KEY([PositionID])
REFERENCES [dbo].[Positions] ([PositionID])
GO
ALTER TABLE [dbo].[SoftwarePosition] CHECK CONSTRAINT [FK_SoftwarePosition_Positions]
GO
ALTER TABLE [dbo].[SoftwarePosition]  WITH CHECK ADD  CONSTRAINT [FK_SoftwarePosition_Software1] FOREIGN KEY([SoftwareID])
REFERENCES [dbo].[Software] ([SoftwareID])
GO
ALTER TABLE [dbo].[SoftwarePosition] CHECK CONSTRAINT [FK_SoftwarePosition_Software1]
GO
