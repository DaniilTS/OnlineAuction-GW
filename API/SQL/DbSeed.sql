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

DECLARE @positive NVARCHAR(10) = N'Positive'
DECLARE @negative NVARCHAR(10) = N'Negative'

IF NOT EXISTS(SELECT * FROM [BalanceOperationType] WHERE [Name]=@positive)
	INSERT INTO [BalanceOperationType] VALUES (NEWID(), @positive, 1)
IF NOT EXISTS(SELECT * FROM [BalanceOperationType] WHERE [Name]=@negative)
	INSERT INTO [BalanceOperationType] VALUES (NEWID(), @negative, 0)
GO

DECLARE @positiveBalanceOperationType UNIQUEIDENTIFIER = (SELECT [Id] FROM [BalanceOperationType] WHERE [IsPositive] = 1)
DECLARE @negativeBalanceOperationType UNIQUEIDENTIFIER = (SELECT [Id] FROM [BalanceOperationType] WHERE [IsPositive] = 0)

DECLARE @add NVARCHAR(10) = N'Add'
DECLARE @withdrawal NVARCHAR(10) = N'Withdrawal'
DECLARE @notice NVARCHAR(10) = N'Notice'

IF NOT EXISTS(SELECT * FROM [FinanceOperationType] WHERE [Name]=@add)
	INSERT INTO [FinanceOperationType] VALUES (NEWID(), @add, @positiveBalanceOperationType)
IF NOT EXISTS(SELECT * FROM [FinanceOperationType] WHERE [Name]=@withdrawal)
	INSERT INTO [FinanceOperationType] VALUES (NEWID(), @withdrawal, @negativeBalanceOperationType)
IF NOT EXISTS(SELECT * FROM [FinanceOperationType] WHERE [Name]=@notice)
	INSERT INTO [FinanceOperationType] VALUES (NEWID(), @notice, @positiveBalanceOperationType)
GO

DECLARE @pending NVARCHAR(10) = N'Pending'
DECLARE @success NVARCHAR(10) = N'Success'

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

DECLARE @accepted NVARCHAR(10) = N'Accepted'
DECLARE @pending NVARCHAR(10) = N'Pending'
DECLARE @declined NVARCHAR(10) = N'Declined'

IF NOT EXISTS(SELECT * FROM [OfferStatus] WHERE [Name]=@accepted)
	INSERT INTO [OfferStatus] VALUES (NEWID(), @accepted)
IF NOT EXISTS(SELECT * FROM [OfferStatus] WHERE [Name]=@pending)
	INSERT INTO [OfferStatus] VALUES (NEWID(), @pending)
IF NOT EXISTS(SELECT * FROM [OfferStatus] WHERE [Name]=@declined)
	INSERT INTO [OfferStatus] VALUES (NEWID(), @declined)
GO

DECLARE @left NVARCHAR(36) = CONVERT(NVARCHAR(36), NEWID())
DECLARE @right NVARCHAR(36) = CONVERT(NVARCHAR(36), NEWID())
DECLARE @salt NVARCHAR(72) = (SELECT CONCAT(@left, @right))
DECLARE @password NVARCHAR(128) = (CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', CONCAT(N'password', @salt)), 2))
DECLARE @adminRole UNIQUEIDENTIFIER = (SELECT [Id] FROM [Role] WHERE [Name]=N'Admin')

IF NOT EXISTS(SELECT * FROM [User] WHERE [RoleId]=@adminRole)
	DECLARE @userId UNIQUEIDENTIFIER = NEWID()
	INSERT INTO [User] VALUES (@userId, @adminRole, NULL, NULL, N'd.tsukrov@gmail.com', N'+375292759056', @password, @salt, NULL, NULL, 0, 0, GETUTCDATE(), GETUTCDATE())
	IF NOT EXISTS(SELECT * FROM [Pocket] WHERE [HolderId]=@userId)
		INSERT INTO [Pocket] VALUES (NEWID(), @userId, 0)
GO

