CREATE TABLE [dbo].[Inventory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ProductId] int not null,
	[Quantity] int not null,
	[PurchasePrice] money not null,
	[PurchaseDate] Datetime2 not null
)
