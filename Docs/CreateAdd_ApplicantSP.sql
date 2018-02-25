USE [Application]
GO

/****** Object:  StoredProcedure [dbo].[Add_Applicant]    Script Date: 2/25/2018 4:43:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




create procedure [dbo].[Add_Applicant] @Name nvarchar(50), @Age int, @Email nvarchar(50),@AboutYou nvarchar(max), 
		@Experience nvarchar(max),@SkillsTalents nvarchar(max),@FileName nvarchar(max)
as

set nocount on

insert into [Application].[dbo].[Applicants]

values (@Name, @Age, @Email, @AboutYou, @Experience, @SKillsTalents, @FileName, 'false'); 


GO

