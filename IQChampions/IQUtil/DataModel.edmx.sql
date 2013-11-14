
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 11/14/2013 11:09:44
-- Generated from EDMX file: C:\Users\hallgato\Documents\GitHub\IQ-Champions\IQChampions\IQUtil\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [aspnet-IQWebApp-20131024110038];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[QuestionsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionsSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [name] nvarchar(max)  NOT NULL,
    [pass] nvarchar(max)  NOT NULL,
    [email] nvarchar(max)  NOT NULL,
    [played] smallint  NOT NULL,
    [win] smallint  NOT NULL,
    [questions] smallint  NOT NULL,
    [goodanswers] smallint  NOT NULL
);
GO

-- Creating table 'QuestionsSet'
CREATE TABLE [dbo].[QuestionsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [question] nvarchar(max)  NOT NULL,
    [goodanswer] nvarchar(max)  NOT NULL,
    [badanswer1] nvarchar(max)  NOT NULL,
    [badanswer2] nvarchar(max)  NOT NULL,
    [badanswer3] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [name] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([name] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionsSet'
ALTER TABLE [dbo].[QuestionsSet]
ADD CONSTRAINT [PK_QuestionsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------