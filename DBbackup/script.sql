USE [ZooDB]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 10.05.2022 18:59:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[login] [varchar](50) NOT NULL,
	[password] [varchar](80) NOT NULL,
	[permission] [int] NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Animal]    Script Date: 10.05.2022 18:59:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Animal](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[birthday] [date] NULL,
	[image] [image] NOT NULL,
	[sex] [varchar](50) NOT NULL,
	[kind] [varchar](50) NOT NULL,
	[breed] [varchar](50) NULL,
 CONSTRAINT [PK_Animal] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 10.05.2022 18:59:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[firstname] [varchar](50) NOT NULL,
	[lastname] [varchar](50) NOT NULL,
	[Age] [int] NOT NULL,
	[position] [varchar](50) NOT NULL,
	[idaccount] [int] NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Worktable]    Script Date: 10.05.2022 18:59:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Worktable](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[worktime] [datetime] NOT NULL,
	[workerid] [int] NOT NULL,
	[animalid] [int] NOT NULL,
 CONSTRAINT [PK_Worktable] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_Account] FOREIGN KEY([idaccount])
REFERENCES [dbo].[Account] ([id])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_Account]
GO
ALTER TABLE [dbo].[Worktable]  WITH CHECK ADD  CONSTRAINT [FK_Worktable_Animal] FOREIGN KEY([animalid])
REFERENCES [dbo].[Animal] ([id])
GO
ALTER TABLE [dbo].[Worktable] CHECK CONSTRAINT [FK_Worktable_Animal]
GO
ALTER TABLE [dbo].[Worktable]  WITH CHECK ADD  CONSTRAINT [FK_Worktable_Person] FOREIGN KEY([workerid])
REFERENCES [dbo].[Person] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Worktable] CHECK CONSTRAINT [FK_Worktable_Person]
GO
