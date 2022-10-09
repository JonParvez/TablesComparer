If(db_id(N'MatchTableDB') IS NULL)
	BEGIN
		CREATE DATABASE [MatchTableDB]
		print('database created')
	END
else
	print('Using [MatchTableDB] ...')
GO
USE [MatchTableDB]
GO
/****** Object:  Table [dbo].[SourceTable2]    Script Date: 10/9/2022 3:47:38 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SourceTable2]') AND type in (N'U'))
DROP TABLE [dbo].[SourceTable2]
GO
/****** Object:  Table [dbo].[SourceTable1]    Script Date: 10/9/2022 3:47:38 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SourceTable1]') AND type in (N'U'))
DROP TABLE [dbo].[SourceTable1]
GO
CREATE TABLE [dbo].[SourceTable1](
	[SocialSecurityNumber] [nvarchar](50) NOT NULL,
	[Firstname] [nvarchar](50) NULL,
	[Lastname] [nvarchar](50) NULL,
	[Department] [nvarchar](50) NULL,
	PRIMARY KEY ([socialsecuritynumber]))
Go
CREATE TABLE [dbo].[SourceTable2](
	[SocialSecurityNumber] [nvarchar](50) NOT NULL,
	[Firstname] [nvarchar](50) NULL,
	[Lastname] [nvarchar](50) NULL,
	[Department] [nvarchar](50) NULL,
	PRIMARY KEY ([socialsecuritynumber]))
GO
INSERT [dbo].[SourceTable1] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010101', N'Kari', N'Nordmann', N'Sales')
GO
INSERT [dbo].[SourceTable1] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010102', N'Jack', N'Jackson', N'Support')
GO
INSERT [dbo].[SourceTable1] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010103', N'Nils', N'Nilsen', N'Sales')
GO
INSERT [dbo].[SourceTable1] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010105', N'Wade', N'Allen', N'Marketing')
GO
INSERT [dbo].[SourceTable1] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010106', N'Mandy', N'Lopez', N'IT')
GO
INSERT [dbo].[SourceTable1] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010108', N'Oscar', N'Brown', N'Development')
GO
INSERT [dbo].[SourceTable1] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010109', N'Jade', N'Blue', N'Sales')
GO
INSERT [dbo].[SourceTable1] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010110', N'Wade', N'Costa', N'Sales')
GO
INSERT [dbo].[SourceTable1] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010111', N'Lary', N'Brown', N'Marketing')
GO
INSERT [dbo].[SourceTable1] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010113', N'Newton', N'Anderson', N'Support')
GO
INSERT [dbo].[SourceTable1] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010114', N'Lucas', N'Gomez', N'Support')
GO
INSERT [dbo].[SourceTable1] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010116', N'Jace', N'Lucy', N'Marketing')
GO
INSERT [dbo].[SourceTable1] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010117', N'Walt', N'Harry', N'Development')
GO
INSERT [dbo].[SourceTable1] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010118', N'Miles', N'Ford', N'Development')
GO
INSERT [dbo].[SourceTable1] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010121', N'Milton', N'Fisher', N'Support')
GO
INSERT [dbo].[SourceTable1] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010123', N'Roman', N'Josh', N'IT')
GO
INSERT [dbo].[SourceTable2] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010101', N'Kari', N'Nordman', N'Support')
GO
INSERT [dbo].[SourceTable2] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010103', N'Nils', N'Nilsen', N'Sales')
GO
INSERT [dbo].[SourceTable2] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010104', N'Esther', N'Doe', N'Support')
GO
INSERT [dbo].[SourceTable2] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010105', N'Wade', N'Nelson', N'IT')
GO
INSERT [dbo].[SourceTable2] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010107', N'Dave', N'Green', N'Sales')
GO
INSERT [dbo].[SourceTable2] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010108', N'Ivan', N'Costa', N'Development')
GO
INSERT [dbo].[SourceTable2] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010109', N'Jade', N'Blue', N'Sales')
GO
INSERT [dbo].[SourceTable2] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010110', N'Wade', N'Costa', N'Sales')
GO
INSERT [dbo].[SourceTable2] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010111', N'Lary', N'Brown', N'IT')
GO
INSERT [dbo].[SourceTable2] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010112', N'Jack', N'Allen', N'Support')
GO
INSERT [dbo].[SourceTable2] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010115', N'Kary', N'Brown', N'Support')
GO
INSERT [dbo].[SourceTable2] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010116', N'Jace', N'Lucy', N'IT')
GO
INSERT [dbo].[SourceTable2] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010118', N'Jace', N'Mandy', N'Development')
GO
INSERT [dbo].[SourceTable2] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010119', N'Brian', N'Warren', N'Builder')
GO
INSERT [dbo].[SourceTable2] ([SocialSecurityNumber], [Firstname], [Lastname], [Department]) VALUES (N'01010120', N'Liam', N'Perez', N'Builder')
GO
