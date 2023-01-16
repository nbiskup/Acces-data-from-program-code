
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/15/2023 11:35:43
-- Generated from EDMX file: C:\Users\Nikola\Desktop\Algebra\Treca_godina\PPPK\NikolaBiskup_PPPK_Projekt\PPPK_Project\PersonVehicleCollection\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PersonVehicle];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PersonSet'
CREATE TABLE [dbo].[PersonSet] (
    [IDPerson] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(20)  NOT NULL,
    [LastName] nvarchar(20)  NOT NULL,
    [Age] int  NOT NULL,
    [Address] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'VehicleSet'
CREATE TABLE [dbo].[VehicleSet] (
    [IDVehicle] int IDENTITY(1,1) NOT NULL,
    [Brand] nvarchar(20)  NOT NULL,
    [Model] nvarchar(20)  NOT NULL,
    [Kilometers] int  NOT NULL,
    [Color] nvarchar(20)  NOT NULL,
    [PersonIDPerson] int  NOT NULL
);
GO

-- Creating table 'UploadedFiles'
CREATE TABLE [dbo].[UploadedFiles] (
    [IDUploadedFile] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(20)  NOT NULL,
    [ContentType] nvarchar(20)  NOT NULL,
    [Content] varbinary(max)  NOT NULL,
    [VehicleIDVehicle] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IDPerson] in table 'PersonSet'
ALTER TABLE [dbo].[PersonSet]
ADD CONSTRAINT [PK_PersonSet]
    PRIMARY KEY CLUSTERED ([IDPerson] ASC);
GO

-- Creating primary key on [IDVehicle] in table 'VehicleSet'
ALTER TABLE [dbo].[VehicleSet]
ADD CONSTRAINT [PK_VehicleSet]
    PRIMARY KEY CLUSTERED ([IDVehicle] ASC);
GO

-- Creating primary key on [IDUploadedFile] in table 'UploadedFiles'
ALTER TABLE [dbo].[UploadedFiles]
ADD CONSTRAINT [PK_UploadedFiles]
    PRIMARY KEY CLUSTERED ([IDUploadedFile] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PersonIDPerson] in table 'VehicleSet'
ALTER TABLE [dbo].[VehicleSet]
ADD CONSTRAINT [FK_PersonVehicle]
    FOREIGN KEY ([PersonIDPerson])
    REFERENCES [dbo].[PersonSet]
        ([IDPerson])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonVehicle'
CREATE INDEX [IX_FK_PersonVehicle]
ON [dbo].[VehicleSet]
    ([PersonIDPerson]);
GO

-- Creating foreign key on [VehicleIDVehicle] in table 'UploadedFiles'
ALTER TABLE [dbo].[UploadedFiles]
ADD CONSTRAINT [FK_VehicleUploadedFile]
    FOREIGN KEY ([VehicleIDVehicle])
    REFERENCES [dbo].[VehicleSet]
        ([IDVehicle])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VehicleUploadedFile'
CREATE INDEX [IX_FK_VehicleUploadedFile]
ON [dbo].[UploadedFiles]
    ([VehicleIDVehicle]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------