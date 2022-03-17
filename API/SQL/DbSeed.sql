USE [OnlineAuction]
GO

DECLARE @admin NVARCHAR(10) = N'Admin'
DECLARE @client NVARCHAR(10) = N'Client'
DECLARE @employee NVARCHAR(10) = N'Employee'

IF NOT EXISTS(SELECT * FROM [Role] WHERE [Name]=@admin)
	INSERT INTO [Role] VALUES (NEWID(), @admin)
IF NOT EXISTS(SELECT * FROM [Role] WHERE [Name]=@client)
	INSERT INTO [Role] VALUES (NEWID(), @client)
IF NOT EXISTS(SELECT * FROM [Role] WHERE [Name]=@employee)
	INSERT INTO [Role] VALUES (NEWID(), @employee)
GO

DECLARE @email NVARCHAR(15) = N'Email'
DECLARE @phone NVARCHAR(15) = N'Phone'

IF NOT EXISTS(SELECT * FROM [IdentityType] WHERE [Name]=@email)
	INSERT INTO [IdentityType] VALUES (NEWID(), @email)
IF NOT EXISTS(SELECT * FROM [IdentityType] WHERE [Name]=@phone)
	INSERT INTO [IdentityType] VALUES (NEWID(), @phone)
GO

DECLARE @male NVARCHAR(10) = N'Male'
DECLARE @female NVARCHAR(10) = N'Female'

IF NOT EXISTS(SELECT * FROM [Gender] WHERE [Name]=@male)
	INSERT INTO [Gender] VALUES (NEWID(), @male)
IF NOT EXISTS(SELECT * FROM [Gender] WHERE [Name]=@female)
	INSERT INTO [Gender] VALUES (NEWID(), @female)
GO

DECLARE @blrRuble NVARCHAR(30) = N'Белорусский рубль'
DECLARE @rusRuble NVARCHAR(30) = N'Российский рубль'
DECLARE @usd NVARCHAR(30) = N'Американский доллар'

IF NOT EXISTS(SELECT * FROM [Currency] WHERE [Name]=@blrRuble)
	INSERT INTO [Currency] VALUES (NEWID(), @blrRuble, N'BYN')
IF NOT EXISTS(SELECT * FROM [Currency] WHERE [Name]=@rusRuble)
	INSERT INTO [Currency] VALUES (NEWID(), @rusRuble, N'RUB')
IF NOT EXISTS(SELECT * FROM [Currency] WHERE [Name]=@usd)
	INSERT INTO [Currency] VALUES (NEWID(), @usd, N'USD')
GO

DECLARE @positive NVARCHAR(10) = N'POSITIVE'
DECLARE @negative NVARCHAR(10) = N'NEGATIVE'

IF NOT EXISTS(SELECT * FROM [BalanceOperationType] WHERE [Name]=@positive)
	INSERT INTO [BalanceOperationType] VALUES (NEWID(), @positive, 1)
IF NOT EXISTS(SELECT * FROM [BalanceOperationType] WHERE [Name]=@negative)
	INSERT INTO [BalanceOperationType] VALUES (NEWID(), @negative, 0)
GO

DECLARE @positiveBalanceOperationType UNIQUEIDENTIFIER = (SELECT [Id] FROM [BalanceOperationType] WHERE [IsPositive] = 1)
DECLARE @negativeBalanceOperationType UNIQUEIDENTIFIER = (SELECT [Id] FROM [BalanceOperationType] WHERE [IsPositive] = 0)

DECLARE @add NVARCHAR(10) = N'ADD'
DECLARE @withdrawal NVARCHAR(10) = N'WITHDRAWAL'
DECLARE @notice NVARCHAR(10) = N'NOTICE'

IF NOT EXISTS(SELECT * FROM [FinanceOperationType] WHERE [Name]=@add)
	INSERT INTO [FinanceOperationType] VALUES (NEWID(), @add, @positiveBalanceOperationType)
IF NOT EXISTS(SELECT * FROM [FinanceOperationType] WHERE [Name]=@withdrawal)
	INSERT INTO [FinanceOperationType] VALUES (NEWID(), @withdrawal, @negativeBalanceOperationType)
IF NOT EXISTS(SELECT * FROM [FinanceOperationType] WHERE [Name]=@notice)
	INSERT INTO [FinanceOperationType] VALUES (NEWID(), @notice, @positiveBalanceOperationType)
GO

DECLARE @pending NVARCHAR(10) = N'PENDING'
DECLARE @success NVARCHAR(10) = N'SUCCESS'

IF NOT EXISTS(SELECT * FROM [FinanceOperationStatus] WHERE [Name]=@pending)
	INSERT INTO [FinanceOperationStatus] VALUES (NEWID(), @pending)
IF NOT EXISTS(SELECT * FROM [FinanceOperationStatus] WHERE [Name]=@success)
	INSERT INTO [FinanceOperationStatus] VALUES (NEWID(), @success)
GO

DECLARE @opened NVARCHAR(10) = N'Opened'
DECLARE @closed NVARCHAR(10) = N'Closed'

IF NOT EXISTS(SELECT * FROM [AuctionType] WHERE [Name]=@opened)
	INSERT INTO [AuctionType] VALUES (NEWID(), @opened)
IF NOT EXISTS(SELECT * FROM [AuctionType] WHERE [Name]=@closed)
	INSERT INTO [AuctionType] VALUES (NEWID(), @closed)
GO

DECLARE @accepted NVARCHAR(10) = N'ACCEPTED'
DECLARE @pending NVARCHAR(10) = N'PENDING'
DECLARE @declined NVARCHAR(10) = N'DECLINED'

IF NOT EXISTS(SELECT * FROM [OfferStatus] WHERE [Name]=@accepted)
	INSERT INTO [OfferStatus] VALUES (NEWID(), @accepted)
IF NOT EXISTS(SELECT * FROM [OfferStatus] WHERE [Name]=@pending)
	INSERT INTO [OfferStatus] VALUES (NEWID(), @pending)
IF NOT EXISTS(SELECT * FROM [OfferStatus] WHERE [Name]=@declined)
	INSERT INTO [OfferStatus] VALUES (NEWID(), @declined)
GO

DECLARE @salt NVARCHAR(36) = (SELECT CONVERT(NVARCHAR(36), NEWID()))
DECLARE @password NVARCHAR(128) = (CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', CONCAT(N'password', @salt)), 2))
DECLARE @adminRole UNIQUEIDENTIFIER = (SELECT [Id] FROM [Role] WHERE [Name]=N'Admin')

IF NOT EXISTS(SELECT * FROM [User] WHERE [RoleId]=@adminRole)
	DECLARE @userId UNIQUEIDENTIFIER = NEWID()
	INSERT INTO [User] VALUES (@userId, @adminRole, NULL, NULL, @password, @salt, NULL, 0, 0, GETUTCDATE(), GETUTCDATE())
	IF NOT EXISTS(SELECT * FROM [Pocket] WHERE [HolderId]=@userId)
		INSERT INTO [Pocket] VALUES (NEWID(), @userId, 0)
GO