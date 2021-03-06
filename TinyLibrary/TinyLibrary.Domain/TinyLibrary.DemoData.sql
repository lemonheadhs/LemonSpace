-- Tiny Library Database Script File
-- DEMO DATA

USE [TinyLibraryDB];
GO

/****** Object:  Table [dbo].[Registrations]    Script Date: 10/17/2010 20:58:23 ******/
DELETE FROM [dbo].[Registrations]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 10/17/2010 20:58:23 ******/
DELETE FROM [dbo].[Books]
GO
/****** Object:  Table [dbo].[Readers]    Script Date: 10/17/2010 20:58:23 ******/
DELETE FROM [dbo].[Readers]
GO
/****** Object:  Table [dbo].[Readers]    Script Date: 10/17/2010 20:58:23 ******/
INSERT [dbo].[Readers] ([Id], [Name], [UserName]) VALUES (N'97572a9e-9e4b-4a60-9262-369f7033a4da', N'DaxNet', N'daxnet')
INSERT [dbo].[Readers] ([Id], [Name], [UserName]) VALUES (N'1952f66a-90f6-42a4-97ae-82186b6a790b', N'James', N'james')
INSERT [dbo].[Readers] ([Id], [Name], [UserName]) VALUES (N'75c217be-7992-4fc1-b2a5-832a09ee3100', N'Sunny Chen', N'acqy')
/****** Object:  Table [dbo].[Books]    Script Date: 10/17/2010 20:58:23 ******/
INSERT [dbo].[Books] ([Id], [Title], [Publisher], [PubDate], [ISBN], [Pages], [Lent]) VALUES (N'0f5aa0b1-66f4-494a-8498-067e7fc2c7f8', N'C# Developer''s Guide to ASP.NET, XML, and ADO.NET', N'Addison Wesley', CAST(0x000091DE00000000 AS DateTime), N'0-672-32155-6', 608, 0)
INSERT [dbo].[Books] ([Id], [Title], [Publisher], [PubDate], [ISBN], [Pages], [Lent]) VALUES (N'bf2376ac-4ac6-479d-8388-1dae4f98d55f', N'Architecting Microsoft .NET Solutions for the Enterprise', N'Microsoft Press', CAST(0x00009B3600000000 AS DateTime), N'0-7356-2609-X', 304, 0)
INSERT [dbo].[Books] ([Id], [Title], [Publisher], [PubDate], [ISBN], [Pages], [Lent]) VALUES (N'3ed63a6d-f336-4c7b-a600-27014f1ded6c', N'A Programmer''s Guide to .NET', N'Addison Wesley', CAST(0x0000926A00000000 AS DateTime), N'0-321-11232-6', 720, 0)
INSERT [dbo].[Books] ([Id], [Title], [Publisher], [PubDate], [ISBN], [Pages], [Lent]) VALUES (N'c558bbab-3f2d-48d0-b46a-2a3bdcf82051', N'Programming Entity Framework, 1st Edition', N'O''Reilly Media, Inc.', CAST(0x00009BAE00000000 AS DateTime), N'978-0-596-52028-1', 832, 0)
INSERT [dbo].[Books] ([Id], [Title], [Publisher], [PubDate], [ISBN], [Pages], [Lent]) VALUES (N'6c3a73f6-57df-409e-af56-2de56776d6e6', N'Essential ASP.NET with Examples in C#', N'Addison Wesley', CAST(0x0000931D00000000 AS DateTime), N'0-201-76040-1', 432, 0)
INSERT [dbo].[Books] ([Id], [Title], [Publisher], [PubDate], [ISBN], [Pages], [Lent]) VALUES (N'4d72f745-e4b3-4e6a-ad4d-511d6bbb0f9e', N'Essential .NET, Volume 1: The Common Language Runtime', N'Addison Wesley', CAST(0x000092B700000000 AS DateTime), N'0-201-73411-7', 432, 0)
INSERT [dbo].[Books] ([Id], [Title], [Publisher], [PubDate], [ISBN], [Pages], [Lent]) VALUES (N'34299c0d-d308-48d1-98be-5b19646e323e', N'Foundations of Object-Oriented Programming Using .NET 2.0 Patterns', N'Apress', CAST(0x000095F200000000 AS DateTime), N'1-59059-540-8', 377, 0)
INSERT [dbo].[Books] ([Id], [Title], [Publisher], [PubDate], [ISBN], [Pages], [Lent]) VALUES (N'a7e986fa-424b-4dd2-b4af-d62f435badd9', N'Programming WCF Services', N'O''Reilly', CAST(0x000098DB00000000 AS DateTime), N'0-596-52699-7', 634, 0)
/****** Object:  Table [dbo].[Registrations]    Script Date: 10/17/2010 20:58:23 ******/
