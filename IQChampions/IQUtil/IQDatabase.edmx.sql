
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 11/20/2013 01:53:24
-- Generated from EDMX file: C:\Users\Ádám\Documents\GitHub\IQ-Champions\IQChampions\IQUtil\IQDatabase.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [IQChampions];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[dbQuestionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[dbQuestionSet];
GO
IF OBJECT_ID(N'[IQChampionsModelStoreContainer].[dbUserSet]', 'U') IS NOT NULL
    DROP TABLE [IQChampionsModelStoreContainer].[dbUserSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'dbQuestionSet'
CREATE TABLE [dbo].[dbQuestionSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [question] nvarchar(max)  NOT NULL,
    [goodanswer] nvarchar(max)  NOT NULL,
    [badanswer1] nvarchar(max)  NOT NULL,
    [badanswer2] nvarchar(max)  NOT NULL,
    [badanswer3] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'dbUserSet'
CREATE TABLE [dbo].[dbUserSet] (
    [name] nvarchar(max)  NOT NULL,
    [pass] nvarchar(max)  NOT NULL,
    [email] nvarchar(max)  NOT NULL,
    [played] smallint  NOT NULL,
    [win] smallint  NOT NULL,
    [questions] smallint  NOT NULL,
    [goodanswers] smallint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'dbQuestionSet'
ALTER TABLE [dbo].[dbQuestionSet]
ADD CONSTRAINT [PK_dbQuestionSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [name], [pass] in table 'dbUserSet'
ALTER TABLE [dbo].[dbUserSet]
ADD CONSTRAINT [PK_dbUserSet]
    PRIMARY KEY CLUSTERED ([name], [pass] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------