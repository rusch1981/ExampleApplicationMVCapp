USE [Application]
GO

/****** Object:  Table [dbo].[Applicants]    Script Date: 2/25/2018 4:43:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Applicants](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Age] [int] NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[AboutYou] [nvarchar](max) NOT NULL,
	[Experience] [nvarchar](max) NOT NULL,
	[SkillsTalents] [nvarchar](max) NOT NULL,
	[FileName] [nvarchar](max) NOT NULL,
	[Complete] [nvarchar](10) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

