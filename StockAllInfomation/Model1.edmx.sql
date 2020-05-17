
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/18/2018 05:38:50
-- Generated from EDMX file: D:\stock\StockAllInfomation\StockAllInfomation\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [myStock];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AssetsAssetsFields]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AssetsSet] DROP CONSTRAINT [FK_AssetsAssetsFields];
GO
IF OBJECT_ID(N'[dbo].[FK_StockAssets]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AssetsSet] DROP CONSTRAINT [FK_StockAssets];
GO
IF OBJECT_ID(N'[dbo].[FK_SessionAssets]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AssetsSet] DROP CONSTRAINT [FK_SessionAssets];
GO
IF OBJECT_ID(N'[dbo].[FK_AssetsFieldsAssetsFieldsGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AssetsFieldsSet] DROP CONSTRAINT [FK_AssetsFieldsAssetsFieldsGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_PerformanceStock]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PerformanceSet] DROP CONSTRAINT [FK_PerformanceStock];
GO
IF OBJECT_ID(N'[dbo].[FK_SessionPerformance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PerformanceSet] DROP CONSTRAINT [FK_SessionPerformance];
GO
IF OBJECT_ID(N'[dbo].[FK_YearMonthSaleMonth]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SaleMonthSet] DROP CONSTRAINT [FK_YearMonthSaleMonth];
GO
IF OBJECT_ID(N'[dbo].[FK_SaleMonthFieldsSaleMonth]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SaleMonthSet] DROP CONSTRAINT [FK_SaleMonthFieldsSaleMonth];
GO
IF OBJECT_ID(N'[dbo].[FK_StockSaleMonth]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SaleMonthSet] DROP CONSTRAINT [FK_StockSaleMonth];
GO
IF OBJECT_ID(N'[dbo].[FK_PerformanceFieldsPerformance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PerformanceSet] DROP CONSTRAINT [FK_PerformanceFieldsPerformance];
GO
IF OBJECT_ID(N'[dbo].[FK_SessionCash]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CashSet] DROP CONSTRAINT [FK_SessionCash];
GO
IF OBJECT_ID(N'[dbo].[FK_CashFieldsCash]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CashSet] DROP CONSTRAINT [FK_CashFieldsCash];
GO
IF OBJECT_ID(N'[dbo].[FK_StockCash]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CashSet] DROP CONSTRAINT [FK_StockCash];
GO
IF OBJECT_ID(N'[dbo].[FK_IncomeFieldGroupIncomeFields]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncomeFieldsSet] DROP CONSTRAINT [FK_IncomeFieldGroupIncomeFields];
GO
IF OBJECT_ID(N'[dbo].[FK_IncomeFieldsIncome]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncomeSet] DROP CONSTRAINT [FK_IncomeFieldsIncome];
GO
IF OBJECT_ID(N'[dbo].[FK_SessionIncome]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncomeSet] DROP CONSTRAINT [FK_SessionIncome];
GO
IF OBJECT_ID(N'[dbo].[FK_StockIncome]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncomeSet] DROP CONSTRAINT [FK_StockIncome];
GO
IF OBJECT_ID(N'[dbo].[FK_StockDividend]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DividendSet] DROP CONSTRAINT [FK_StockDividend];
GO
IF OBJECT_ID(N'[dbo].[FK_YearDividend]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DividendSet] DROP CONSTRAINT [FK_YearDividend];
GO
IF OBJECT_ID(N'[dbo].[FK_DivendendFieldsDividend]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DividendSet] DROP CONSTRAINT [FK_DivendendFieldsDividend];
GO
IF OBJECT_ID(N'[dbo].[FK_YearCash]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CashSet] DROP CONSTRAINT [FK_YearCash];
GO
IF OBJECT_ID(N'[dbo].[FK_YearIncome]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncomeSet] DROP CONSTRAINT [FK_YearIncome];
GO
IF OBJECT_ID(N'[dbo].[FK_YearSaleMonth]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SaleMonthSet] DROP CONSTRAINT [FK_YearSaleMonth];
GO
IF OBJECT_ID(N'[dbo].[FK_YearPerformance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PerformanceSet] DROP CONSTRAINT [FK_YearPerformance];
GO
IF OBJECT_ID(N'[dbo].[FK_YearAssets]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AssetsSet] DROP CONSTRAINT [FK_YearAssets];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[StockSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StockSet];
GO
IF OBJECT_ID(N'[dbo].[AssetsFieldsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AssetsFieldsSet];
GO
IF OBJECT_ID(N'[dbo].[AssetsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AssetsSet];
GO
IF OBJECT_ID(N'[dbo].[SessionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SessionSet];
GO
IF OBJECT_ID(N'[dbo].[AssetsFieldsGroupSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AssetsFieldsGroupSet];
GO
IF OBJECT_ID(N'[dbo].[PerformanceSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PerformanceSet];
GO
IF OBJECT_ID(N'[dbo].[PerformanceFieldsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PerformanceFieldsSet];
GO
IF OBJECT_ID(N'[dbo].[MonthSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MonthSet];
GO
IF OBJECT_ID(N'[dbo].[SaleMonthSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SaleMonthSet];
GO
IF OBJECT_ID(N'[dbo].[SaleMonthFieldsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SaleMonthFieldsSet];
GO
IF OBJECT_ID(N'[dbo].[CashSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CashSet];
GO
IF OBJECT_ID(N'[dbo].[CashFieldsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CashFieldsSet];
GO
IF OBJECT_ID(N'[dbo].[IncomeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncomeSet];
GO
IF OBJECT_ID(N'[dbo].[IncomeFieldsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncomeFieldsSet];
GO
IF OBJECT_ID(N'[dbo].[IncomeFieldGroupSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncomeFieldGroupSet];
GO
IF OBJECT_ID(N'[dbo].[YearSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[YearSet];
GO
IF OBJECT_ID(N'[dbo].[DividendSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DividendSet];
GO
IF OBJECT_ID(N'[dbo].[DivendendFieldsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DivendendFieldsSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'StockSet'
CREATE TABLE [dbo].[StockSet] (
    [idStock] int  NOT NULL,
    [nameStock] nvarchar(max)  NOT NULL,
    [isCompany] bit  NOT NULL
);
GO

-- Creating table 'AssetsFieldsSet'
CREATE TABLE [dbo].[AssetsFieldsSet] (
    [idAssetsField] int  NOT NULL,
    [nameAssetsField] nvarchar(max)  NOT NULL,
    [idAssetsFieldGroup] int  NOT NULL
);
GO

-- Creating table 'AssetsSet'
CREATE TABLE [dbo].[AssetsSet] (
    [idAssets] int IDENTITY(1,1) NOT NULL,
    [idAssetsField] int  NOT NULL,
    [cost] nvarchar(max)  NULL,
    [percentage] nvarchar(max)  NULL,
    [idStock] int  NOT NULL,
    [idSession] int  NOT NULL,
    [idYear] int  NOT NULL
);
GO

-- Creating table 'SessionSet'
CREATE TABLE [dbo].[SessionSet] (
    [idSession] int  NOT NULL,
    [nameSession] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AssetsFieldsGroupSet'
CREATE TABLE [dbo].[AssetsFieldsGroupSet] (
    [idAssetsFieldGroup] int  NOT NULL,
    [nameAssetsFieldGroup] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PerformanceSet'
CREATE TABLE [dbo].[PerformanceSet] (
    [idPerformance] int IDENTITY(1,1) NOT NULL,
    [idStock] int  NOT NULL,
    [idSession] int  NOT NULL,
    [idPeformanceField] int  NOT NULL,
    [valuePerformance] nvarchar(max)  NOT NULL,
    [idYear] int  NOT NULL
);
GO

-- Creating table 'PerformanceFieldsSet'
CREATE TABLE [dbo].[PerformanceFieldsSet] (
    [idPerformanceField] int  NOT NULL,
    [namePerformanceField] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MonthSet'
CREATE TABLE [dbo].[MonthSet] (
    [idMonth] int  NOT NULL,
    [nameMonth] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SaleMonthSet'
CREATE TABLE [dbo].[SaleMonthSet] (
    [idSaleMonth] int IDENTITY(1,1) NOT NULL,
    [idMonth] int  NOT NULL,
    [idStock] int  NOT NULL,
    [idSaleMonthField] int  NOT NULL,
    [valueSaleMonth] nvarchar(max)  NOT NULL,
    [idYear] int  NOT NULL
);
GO

-- Creating table 'SaleMonthFieldsSet'
CREATE TABLE [dbo].[SaleMonthFieldsSet] (
    [idSaleMonthField] int  NOT NULL,
    [nameSaleMonthField] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CashSet'
CREATE TABLE [dbo].[CashSet] (
    [idCash] int IDENTITY(1,1) NOT NULL,
    [idStock] int  NOT NULL,
    [idSession] int  NOT NULL,
    [idCashField] int  NOT NULL,
    [valueCash] nvarchar(max)  NOT NULL,
    [idYear] int  NOT NULL
);
GO

-- Creating table 'CashFieldsSet'
CREATE TABLE [dbo].[CashFieldsSet] (
    [idCashField] int  NOT NULL,
    [nameCashField] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'IncomeSet'
CREATE TABLE [dbo].[IncomeSet] (
    [idIncome] int IDENTITY(1,1) NOT NULL,
    [idStock] int  NOT NULL,
    [idSession] int  NOT NULL,
    [idIncomeField] int  NOT NULL,
    [cost] nvarchar(max)  NOT NULL,
    [percentage] nvarchar(max)  NOT NULL,
    [idYear] int  NOT NULL
);
GO

-- Creating table 'IncomeFieldsSet'
CREATE TABLE [dbo].[IncomeFieldsSet] (
    [idIncomeField] int  NOT NULL,
    [nameIncomeField] nvarchar(max)  NOT NULL,
    [idIncomFieldGroup] int  NOT NULL
);
GO

-- Creating table 'IncomeFieldGroupSet'
CREATE TABLE [dbo].[IncomeFieldGroupSet] (
    [idIncomeFieldGroup] int  NOT NULL,
    [nameIncomeFieldGroup] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'YearSet'
CREATE TABLE [dbo].[YearSet] (
    [idYear] int IDENTITY(1,1) NOT NULL,
    [nameYear] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DividendSet'
CREATE TABLE [dbo].[DividendSet] (
    [idDividend] int IDENTITY(1,1) NOT NULL,
    [idStock] int  NOT NULL,
    [idYear] int  NOT NULL,
    [idDividendField] int  NOT NULL,
    [valueDividend] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DivendendFieldsSet'
CREATE TABLE [dbo].[DivendendFieldsSet] (
    [idDividendField] int  NOT NULL,
    [nameDividendField] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [idStock] in table 'StockSet'
ALTER TABLE [dbo].[StockSet]
ADD CONSTRAINT [PK_StockSet]
    PRIMARY KEY CLUSTERED ([idStock] ASC);
GO

-- Creating primary key on [idAssetsField] in table 'AssetsFieldsSet'
ALTER TABLE [dbo].[AssetsFieldsSet]
ADD CONSTRAINT [PK_AssetsFieldsSet]
    PRIMARY KEY CLUSTERED ([idAssetsField] ASC);
GO

-- Creating primary key on [idAssets] in table 'AssetsSet'
ALTER TABLE [dbo].[AssetsSet]
ADD CONSTRAINT [PK_AssetsSet]
    PRIMARY KEY CLUSTERED ([idAssets] ASC);
GO

-- Creating primary key on [idSession] in table 'SessionSet'
ALTER TABLE [dbo].[SessionSet]
ADD CONSTRAINT [PK_SessionSet]
    PRIMARY KEY CLUSTERED ([idSession] ASC);
GO

-- Creating primary key on [idAssetsFieldGroup] in table 'AssetsFieldsGroupSet'
ALTER TABLE [dbo].[AssetsFieldsGroupSet]
ADD CONSTRAINT [PK_AssetsFieldsGroupSet]
    PRIMARY KEY CLUSTERED ([idAssetsFieldGroup] ASC);
GO

-- Creating primary key on [idPerformance] in table 'PerformanceSet'
ALTER TABLE [dbo].[PerformanceSet]
ADD CONSTRAINT [PK_PerformanceSet]
    PRIMARY KEY CLUSTERED ([idPerformance] ASC);
GO

-- Creating primary key on [idPerformanceField] in table 'PerformanceFieldsSet'
ALTER TABLE [dbo].[PerformanceFieldsSet]
ADD CONSTRAINT [PK_PerformanceFieldsSet]
    PRIMARY KEY CLUSTERED ([idPerformanceField] ASC);
GO

-- Creating primary key on [idMonth] in table 'MonthSet'
ALTER TABLE [dbo].[MonthSet]
ADD CONSTRAINT [PK_MonthSet]
    PRIMARY KEY CLUSTERED ([idMonth] ASC);
GO

-- Creating primary key on [idSaleMonth] in table 'SaleMonthSet'
ALTER TABLE [dbo].[SaleMonthSet]
ADD CONSTRAINT [PK_SaleMonthSet]
    PRIMARY KEY CLUSTERED ([idSaleMonth] ASC);
GO

-- Creating primary key on [idSaleMonthField] in table 'SaleMonthFieldsSet'
ALTER TABLE [dbo].[SaleMonthFieldsSet]
ADD CONSTRAINT [PK_SaleMonthFieldsSet]
    PRIMARY KEY CLUSTERED ([idSaleMonthField] ASC);
GO

-- Creating primary key on [idCash] in table 'CashSet'
ALTER TABLE [dbo].[CashSet]
ADD CONSTRAINT [PK_CashSet]
    PRIMARY KEY CLUSTERED ([idCash] ASC);
GO

-- Creating primary key on [idCashField] in table 'CashFieldsSet'
ALTER TABLE [dbo].[CashFieldsSet]
ADD CONSTRAINT [PK_CashFieldsSet]
    PRIMARY KEY CLUSTERED ([idCashField] ASC);
GO

-- Creating primary key on [idIncome] in table 'IncomeSet'
ALTER TABLE [dbo].[IncomeSet]
ADD CONSTRAINT [PK_IncomeSet]
    PRIMARY KEY CLUSTERED ([idIncome] ASC);
GO

-- Creating primary key on [idIncomeField] in table 'IncomeFieldsSet'
ALTER TABLE [dbo].[IncomeFieldsSet]
ADD CONSTRAINT [PK_IncomeFieldsSet]
    PRIMARY KEY CLUSTERED ([idIncomeField] ASC);
GO

-- Creating primary key on [idIncomeFieldGroup] in table 'IncomeFieldGroupSet'
ALTER TABLE [dbo].[IncomeFieldGroupSet]
ADD CONSTRAINT [PK_IncomeFieldGroupSet]
    PRIMARY KEY CLUSTERED ([idIncomeFieldGroup] ASC);
GO

-- Creating primary key on [idYear] in table 'YearSet'
ALTER TABLE [dbo].[YearSet]
ADD CONSTRAINT [PK_YearSet]
    PRIMARY KEY CLUSTERED ([idYear] ASC);
GO

-- Creating primary key on [idDividend] in table 'DividendSet'
ALTER TABLE [dbo].[DividendSet]
ADD CONSTRAINT [PK_DividendSet]
    PRIMARY KEY CLUSTERED ([idDividend] ASC);
GO

-- Creating primary key on [idDividendField] in table 'DivendendFieldsSet'
ALTER TABLE [dbo].[DivendendFieldsSet]
ADD CONSTRAINT [PK_DivendendFieldsSet]
    PRIMARY KEY CLUSTERED ([idDividendField] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [idAssetsField] in table 'AssetsSet'
ALTER TABLE [dbo].[AssetsSet]
ADD CONSTRAINT [FK_AssetsAssetsFields]
    FOREIGN KEY ([idAssetsField])
    REFERENCES [dbo].[AssetsFieldsSet]
        ([idAssetsField])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AssetsAssetsFields'
CREATE INDEX [IX_FK_AssetsAssetsFields]
ON [dbo].[AssetsSet]
    ([idAssetsField]);
GO

-- Creating foreign key on [idStock] in table 'AssetsSet'
ALTER TABLE [dbo].[AssetsSet]
ADD CONSTRAINT [FK_StockAssets]
    FOREIGN KEY ([idStock])
    REFERENCES [dbo].[StockSet]
        ([idStock])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StockAssets'
CREATE INDEX [IX_FK_StockAssets]
ON [dbo].[AssetsSet]
    ([idStock]);
GO

-- Creating foreign key on [idSession] in table 'AssetsSet'
ALTER TABLE [dbo].[AssetsSet]
ADD CONSTRAINT [FK_SessionAssets]
    FOREIGN KEY ([idSession])
    REFERENCES [dbo].[SessionSet]
        ([idSession])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SessionAssets'
CREATE INDEX [IX_FK_SessionAssets]
ON [dbo].[AssetsSet]
    ([idSession]);
GO

-- Creating foreign key on [idAssetsFieldGroup] in table 'AssetsFieldsSet'
ALTER TABLE [dbo].[AssetsFieldsSet]
ADD CONSTRAINT [FK_AssetsFieldsAssetsFieldsGroup]
    FOREIGN KEY ([idAssetsFieldGroup])
    REFERENCES [dbo].[AssetsFieldsGroupSet]
        ([idAssetsFieldGroup])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AssetsFieldsAssetsFieldsGroup'
CREATE INDEX [IX_FK_AssetsFieldsAssetsFieldsGroup]
ON [dbo].[AssetsFieldsSet]
    ([idAssetsFieldGroup]);
GO

-- Creating foreign key on [idStock] in table 'PerformanceSet'
ALTER TABLE [dbo].[PerformanceSet]
ADD CONSTRAINT [FK_PerformanceStock]
    FOREIGN KEY ([idStock])
    REFERENCES [dbo].[StockSet]
        ([idStock])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PerformanceStock'
CREATE INDEX [IX_FK_PerformanceStock]
ON [dbo].[PerformanceSet]
    ([idStock]);
GO

-- Creating foreign key on [idSession] in table 'PerformanceSet'
ALTER TABLE [dbo].[PerformanceSet]
ADD CONSTRAINT [FK_SessionPerformance]
    FOREIGN KEY ([idSession])
    REFERENCES [dbo].[SessionSet]
        ([idSession])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SessionPerformance'
CREATE INDEX [IX_FK_SessionPerformance]
ON [dbo].[PerformanceSet]
    ([idSession]);
GO

-- Creating foreign key on [idMonth] in table 'SaleMonthSet'
ALTER TABLE [dbo].[SaleMonthSet]
ADD CONSTRAINT [FK_YearMonthSaleMonth]
    FOREIGN KEY ([idMonth])
    REFERENCES [dbo].[MonthSet]
        ([idMonth])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_YearMonthSaleMonth'
CREATE INDEX [IX_FK_YearMonthSaleMonth]
ON [dbo].[SaleMonthSet]
    ([idMonth]);
GO

-- Creating foreign key on [idSaleMonthField] in table 'SaleMonthSet'
ALTER TABLE [dbo].[SaleMonthSet]
ADD CONSTRAINT [FK_SaleMonthFieldsSaleMonth]
    FOREIGN KEY ([idSaleMonthField])
    REFERENCES [dbo].[SaleMonthFieldsSet]
        ([idSaleMonthField])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SaleMonthFieldsSaleMonth'
CREATE INDEX [IX_FK_SaleMonthFieldsSaleMonth]
ON [dbo].[SaleMonthSet]
    ([idSaleMonthField]);
GO

-- Creating foreign key on [idStock] in table 'SaleMonthSet'
ALTER TABLE [dbo].[SaleMonthSet]
ADD CONSTRAINT [FK_StockSaleMonth]
    FOREIGN KEY ([idStock])
    REFERENCES [dbo].[StockSet]
        ([idStock])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StockSaleMonth'
CREATE INDEX [IX_FK_StockSaleMonth]
ON [dbo].[SaleMonthSet]
    ([idStock]);
GO

-- Creating foreign key on [idPeformanceField] in table 'PerformanceSet'
ALTER TABLE [dbo].[PerformanceSet]
ADD CONSTRAINT [FK_PerformanceFieldsPerformance]
    FOREIGN KEY ([idPeformanceField])
    REFERENCES [dbo].[PerformanceFieldsSet]
        ([idPerformanceField])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PerformanceFieldsPerformance'
CREATE INDEX [IX_FK_PerformanceFieldsPerformance]
ON [dbo].[PerformanceSet]
    ([idPeformanceField]);
GO

-- Creating foreign key on [idSession] in table 'CashSet'
ALTER TABLE [dbo].[CashSet]
ADD CONSTRAINT [FK_SessionCash]
    FOREIGN KEY ([idSession])
    REFERENCES [dbo].[SessionSet]
        ([idSession])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SessionCash'
CREATE INDEX [IX_FK_SessionCash]
ON [dbo].[CashSet]
    ([idSession]);
GO

-- Creating foreign key on [idCashField] in table 'CashSet'
ALTER TABLE [dbo].[CashSet]
ADD CONSTRAINT [FK_CashFieldsCash]
    FOREIGN KEY ([idCashField])
    REFERENCES [dbo].[CashFieldsSet]
        ([idCashField])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CashFieldsCash'
CREATE INDEX [IX_FK_CashFieldsCash]
ON [dbo].[CashSet]
    ([idCashField]);
GO

-- Creating foreign key on [idStock] in table 'CashSet'
ALTER TABLE [dbo].[CashSet]
ADD CONSTRAINT [FK_StockCash]
    FOREIGN KEY ([idStock])
    REFERENCES [dbo].[StockSet]
        ([idStock])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StockCash'
CREATE INDEX [IX_FK_StockCash]
ON [dbo].[CashSet]
    ([idStock]);
GO

-- Creating foreign key on [idIncomFieldGroup] in table 'IncomeFieldsSet'
ALTER TABLE [dbo].[IncomeFieldsSet]
ADD CONSTRAINT [FK_IncomeFieldGroupIncomeFields]
    FOREIGN KEY ([idIncomFieldGroup])
    REFERENCES [dbo].[IncomeFieldGroupSet]
        ([idIncomeFieldGroup])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_IncomeFieldGroupIncomeFields'
CREATE INDEX [IX_FK_IncomeFieldGroupIncomeFields]
ON [dbo].[IncomeFieldsSet]
    ([idIncomFieldGroup]);
GO

-- Creating foreign key on [idIncomeField] in table 'IncomeSet'
ALTER TABLE [dbo].[IncomeSet]
ADD CONSTRAINT [FK_IncomeFieldsIncome]
    FOREIGN KEY ([idIncomeField])
    REFERENCES [dbo].[IncomeFieldsSet]
        ([idIncomeField])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_IncomeFieldsIncome'
CREATE INDEX [IX_FK_IncomeFieldsIncome]
ON [dbo].[IncomeSet]
    ([idIncomeField]);
GO

-- Creating foreign key on [idSession] in table 'IncomeSet'
ALTER TABLE [dbo].[IncomeSet]
ADD CONSTRAINT [FK_SessionIncome]
    FOREIGN KEY ([idSession])
    REFERENCES [dbo].[SessionSet]
        ([idSession])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SessionIncome'
CREATE INDEX [IX_FK_SessionIncome]
ON [dbo].[IncomeSet]
    ([idSession]);
GO

-- Creating foreign key on [idStock] in table 'IncomeSet'
ALTER TABLE [dbo].[IncomeSet]
ADD CONSTRAINT [FK_StockIncome]
    FOREIGN KEY ([idStock])
    REFERENCES [dbo].[StockSet]
        ([idStock])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StockIncome'
CREATE INDEX [IX_FK_StockIncome]
ON [dbo].[IncomeSet]
    ([idStock]);
GO

-- Creating foreign key on [idStock] in table 'DividendSet'
ALTER TABLE [dbo].[DividendSet]
ADD CONSTRAINT [FK_StockDividend]
    FOREIGN KEY ([idStock])
    REFERENCES [dbo].[StockSet]
        ([idStock])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StockDividend'
CREATE INDEX [IX_FK_StockDividend]
ON [dbo].[DividendSet]
    ([idStock]);
GO

-- Creating foreign key on [idYear] in table 'DividendSet'
ALTER TABLE [dbo].[DividendSet]
ADD CONSTRAINT [FK_YearDividend]
    FOREIGN KEY ([idYear])
    REFERENCES [dbo].[YearSet]
        ([idYear])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_YearDividend'
CREATE INDEX [IX_FK_YearDividend]
ON [dbo].[DividendSet]
    ([idYear]);
GO

-- Creating foreign key on [idDividendField] in table 'DividendSet'
ALTER TABLE [dbo].[DividendSet]
ADD CONSTRAINT [FK_DivendendFieldsDividend]
    FOREIGN KEY ([idDividendField])
    REFERENCES [dbo].[DivendendFieldsSet]
        ([idDividendField])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DivendendFieldsDividend'
CREATE INDEX [IX_FK_DivendendFieldsDividend]
ON [dbo].[DividendSet]
    ([idDividendField]);
GO

-- Creating foreign key on [idYear] in table 'CashSet'
ALTER TABLE [dbo].[CashSet]
ADD CONSTRAINT [FK_YearCash]
    FOREIGN KEY ([idYear])
    REFERENCES [dbo].[YearSet]
        ([idYear])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_YearCash'
CREATE INDEX [IX_FK_YearCash]
ON [dbo].[CashSet]
    ([idYear]);
GO

-- Creating foreign key on [idYear] in table 'IncomeSet'
ALTER TABLE [dbo].[IncomeSet]
ADD CONSTRAINT [FK_YearIncome]
    FOREIGN KEY ([idYear])
    REFERENCES [dbo].[YearSet]
        ([idYear])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_YearIncome'
CREATE INDEX [IX_FK_YearIncome]
ON [dbo].[IncomeSet]
    ([idYear]);
GO

-- Creating foreign key on [idYear] in table 'SaleMonthSet'
ALTER TABLE [dbo].[SaleMonthSet]
ADD CONSTRAINT [FK_YearSaleMonth]
    FOREIGN KEY ([idYear])
    REFERENCES [dbo].[YearSet]
        ([idYear])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_YearSaleMonth'
CREATE INDEX [IX_FK_YearSaleMonth]
ON [dbo].[SaleMonthSet]
    ([idYear]);
GO

-- Creating foreign key on [idYear] in table 'PerformanceSet'
ALTER TABLE [dbo].[PerformanceSet]
ADD CONSTRAINT [FK_YearPerformance]
    FOREIGN KEY ([idYear])
    REFERENCES [dbo].[YearSet]
        ([idYear])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_YearPerformance'
CREATE INDEX [IX_FK_YearPerformance]
ON [dbo].[PerformanceSet]
    ([idYear]);
GO

-- Creating foreign key on [idYear] in table 'AssetsSet'
ALTER TABLE [dbo].[AssetsSet]
ADD CONSTRAINT [FK_YearAssets]
    FOREIGN KEY ([idYear])
    REFERENCES [dbo].[YearSet]
        ([idYear])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_YearAssets'
CREATE INDEX [IX_FK_YearAssets]
ON [dbo].[AssetsSet]
    ([idYear]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------