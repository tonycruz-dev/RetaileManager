﻿CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ProductName] NVARCHAR(100) NOT NULL,
	[Discription] NVARCHAR(MAX) NOT NULL,
	[RetailPrice] money not null,
	[QuantityInStock] INT NOT NULL DEFAULT 1,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(),
	[LastModified] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    [IsTaxable] BIT NOT NULL DEFAULT 1, 
    
)
