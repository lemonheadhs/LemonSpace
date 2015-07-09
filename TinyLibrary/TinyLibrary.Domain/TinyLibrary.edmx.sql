
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 10/16/2010 21:43:54
-- Generated from EDMX file: D:\My Documents\Visual Studio 2010\Projects\TinyLibrary\TinyLibrary.Domain\TinyLibrary.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TinyLibraryDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ReaderRegistration]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Registrations] DROP CONSTRAINT [FK_ReaderRegistration];
GO
IF OBJECT_ID(N'[dbo].[FK_RegistrationBook]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Registrations] DROP CONSTRAINT [FK_RegistrationBook];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Books]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Books];
GO
IF OBJECT_ID(N'[dbo].[Readers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Readers];
GO
IF OBJECT_ID(N'[dbo].[Registrations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Registrations];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Books'
CREATE TABLE [dbo].[Books] (
    [Id] uniqueidentifier  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Publisher] nvarchar(max)  NOT NULL,
    [PubDate] datetime  NOT NULL,
    [ISBN] nvarchar(max)  NOT NULL,
    [Pages] int  NOT NULL,
    [Lent] bit  NOT NULL
);
GO

-- Creating table 'Readers'
CREATE TABLE [dbo].[Readers] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [UserName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Registrations'
CREATE TABLE [dbo].[Registrations] (
    [Id] uniqueidentifier  NOT NULL,
    [Date] datetime  NOT NULL,
    [DueDate] datetime  NOT NULL,
    [Status] int  NOT NULL,
    [ReturnDate] datetime  NOT NULL,
    [Reader_Id] uniqueidentifier  NOT NULL,
    [Book_Id] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Books'
ALTER TABLE [dbo].[Books]
ADD CONSTRAINT [PK_Books]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Readers'
ALTER TABLE [dbo].[Readers]
ADD CONSTRAINT [PK_Readers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Registrations'
ALTER TABLE [dbo].[Registrations]
ADD CONSTRAINT [PK_Registrations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Reader_Id] in table 'Registrations'
ALTER TABLE [dbo].[Registrations]
ADD CONSTRAINT [FK_ReaderRegistration]
    FOREIGN KEY ([Reader_Id])
    REFERENCES [dbo].[Readers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ReaderRegistration'
CREATE INDEX [IX_FK_ReaderRegistration]
ON [dbo].[Registrations]
    ([Reader_Id]);
GO

-- Creating foreign key on [Book_Id] in table 'Registrations'
ALTER TABLE [dbo].[Registrations]
ADD CONSTRAINT [FK_BookRegistration]
    FOREIGN KEY ([Book_Id])
    REFERENCES [dbo].[Books]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BookRegistration'
CREATE INDEX [IX_FK_BookRegistration]
ON [dbo].[Registrations]
    ([Book_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------