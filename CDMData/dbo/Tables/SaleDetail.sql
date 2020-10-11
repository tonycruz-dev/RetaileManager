CREATE TABLE [dbo].[SaleDetail]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[SaleId] INT NOT NULL,
	[ProductId] INT NOT NULL,
	[Quantity] INT NOT NULL,
	[PurchasePrice] Money not null,
	[Tax] Money not null default 0
)
