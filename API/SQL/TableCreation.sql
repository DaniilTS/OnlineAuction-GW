IF NOT EXISTS (SELECT * FROM SYS.DATABASES WHERE [Name] = 'OnlineAuction')
	CREATE DATABASE [OnlineAuction]
GO

USE OnlineAuction
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='Role')
CREATE TABLE [Role]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(10) UNIQUE NOT NULL
)
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='Gender')
CREATE TABLE [Gender]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(10) UNIQUE NOT NULL
)
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='FullName')
CREATE TABLE [FullName]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[FirstName] NVARCHAR(50) NOT NULL,
	[SecondName] NVARCHAR(50) NOT NULL,
	[ThirdName] NVARCHAR(50) NULL
)
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='User')
CREATE TABLE [User] 
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[RoleId] UNIQUEIDENTIFIER REFERENCES [Role] (Id) NOT NULL,
	[GenderId] UNIQUEIDENTIFIER REFERENCES [Gender] (Id),
	[FullNameId] UNIQUEIDENTIFIER REFERENCES [FullName] (Id),
	[Email] NVARCHAR(255) NOT NULL,
	[Phone] NCHAR(13) NOT NULL,
	[Password] NVARCHAR(255) NOT NULL,
	[RefreshToken] NVARCHAR(255) NULL,
	[Salt] NVARCHAR(72) NOT NULL,
	[PhotoUrl] NVARCHAR(MAX) NULL,
	[IsBlocked] BIT NOT NULL DEFAULT(0),
	[IsDeleted] BIT NOT NULL DEFAULT(0),
	[Created] DATETIME NOT NULL DEFAULT (GETUTCDATE()),
	[Updated] DATETIME NOT NULL DEFAULT(GETUTCDATE())
)
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='UserImage')
CREATE TABLE [UserImage]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[UserId] UNIQUEIDENTIFIER REFERENCES [User] (Id) NULL,
	[Url] NVARCHAR(MAX) NOT NULL,
	[IsDeleted] BIT NOT NULL DEFAULT(0),
	[Created] DATETIME NOT NULL DEFAULT(GETUTCDATE()),
	[Deleted] DATETIME NOT NULL DEFAULT(GETUTCDATE())
)
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='Currency')
CREATE TABLE [Currency]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(30) UNIQUE NOT NULL,
	[Code] CHAR(3) UNIQUE NOT NULL
)
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='CurrencyPair')
CREATE TABLE [CurrencyPair]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[FromId] UNIQUEIDENTIFIER REFERENCES [Currency] (Id) NOT NULL,
	[ToId] UNIQUEIDENTIFIER REFERENCES [Currency] (Id) NOT NULL
)
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='CurrencyPairRate')
CREATE TABLE [CurrencyPairRate]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[CurrencyPairRateId] UNIQUEIDENTIFIER REFERENCES [CurrencyPair] (Id) NOT NULL,
	[Rate] MONEY NOT NULL,
	[RateTime] DATETIME NOT NULL
)
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='Pocket')
CREATE TABLE [Pocket]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[HolderId] UNIQUEIDENTIFIER REFERENCES [User] (Id) NOT NULL,
	[Amount] MONEY NOT NULL DEFAULT(0)
)
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='BalanceOperationType')
CREATE TABLE [BalanceOperationType]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(10) UNIQUE NOT NULL,
	[IsPositive] BIT NOT NULL,
)
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='FinanceOperationType')
CREATE TABLE [FinanceOperationType] -- ADD, NOTICE, WITHDRAWAL
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(10) UNIQUE NOT NULL,
	[BalanceOperationTypeId] UNIQUEIDENTIFIER REFERENCES [BalanceOperationType] (Id) NOT NULL
)
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='FinanceOperationStatus')
CREATE TABLE [FinanceOperationStatus]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(10) UNIQUE NOT NULL,
)
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='FinanceOperation')
CREATE TABLE [FinanceOperation]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[PocketId] UNIQUEIDENTIFIER REFERENCES [Pocket] (Id) NOT NULL,
	[FinanceOperationTypeId] UNIQUEIDENTIFIER REFERENCES [FinanceOperationType] (Id) NOT NULL,
	[FinanceOperationStatusId] UNIQUEIDENTIFIER REFERENCES [FinanceOperationStatus] (Id) NOT NULL,
	[Amount] MONEY NOT NULL DEFAULT(0),
	[Created] DATETIME NOT NULL DEFAULT(GETUTCDATE()),
	[Updated] DATETIME NOT NULL DEFAULT(GETUTCDATE())
)
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='LotCategory')
CREATE TABLE [LotCategory]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(50) UNIQUE NOT NULL
)
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='Lot')
CREATE TABLE [Lot]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[LotCategoryId] UNIQUEIDENTIFIER REFERENCES [LotCategory] (Id) NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL,
	[CreatorId] UNIQUEIDENTIFIER REFERENCES [User] NOT NULL,
	[StartPrice] MONEY NOT NULL,
	[IsSubmitted] BIT NOT NULL DEFAULT(0),
)
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='LotImage')
CREATE TABLE [LotImage]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[LotId] UNIQUEIDENTIFIER REFERENCES [Lot] (Id) NULL,
	[Url] NVARCHAR(MAX) NOT NULL,
	[IsDeleted] BIT NOT NULL DEFAULT(0),
	[Created] DATETIME NOT NULL DEFAULT(GETUTCDATE()),
	[Deleted] DATETIME NOT NULL DEFAULT(GETUTCDATE())
)
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='AuctionType')
CREATE TABLE [AuctionType]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(10) UNIQUE NOT NULL
)
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='Auction')
CREATE TABLE [Auction]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[AuctionTypeId] UNIQUEIDENTIFIER REFERENCES [AuctionType] (Id) NOT NULL,
	[LotId] UNIQUEIDENTIFIER REFERENCES [Lot] (Id) NOT NULL,
	[Start] DATETIME NOT NULL,
	[End] DATETIME NOT NULL,
	[WinnerId] UNIQUEIDENTIFIER REFERENCES [User] NULL,
	[EndPrice] MONEY NULL,
	[FinanceOperationId] UNIQUEIDENTIFIER REFERENCES [FinanceOperation] (Id) DEFAULT(NULL),
	[IsEmailMessageSended] BIT NOT NULL DEFAULT(0),
	[Created] DATETIME NOT NULL
)
GO



IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='OfferStatus')
CREATE TABLE [OfferStatus]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(10) UNIQUE NOT NULL
)
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='Offer')
CREATE TABLE [Offer]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[CreatorId] UNIQUEIDENTIFIER REFERENCES [User] (Id) NOT NULL,
	[LotId] UNIQUEIDENTIFIER REFERENCES [Lot] (Id) NOT NULL,
	[Amount] MONEY NOT NULL,
	[OfferStatusId] UNIQUEIDENTIFIER NOT NULL,
	[FinanceOperationId] UNIQUEIDENTIFIER REFERENCES [FinanceOperation] (Id) DEFAULT(NULL),
	[Created] DATETIME NOT NULL
)
GO