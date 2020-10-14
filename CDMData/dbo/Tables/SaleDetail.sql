CREATE TABLE [dbo].[SaleDetail]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[SaleId] INT NOT NULL,
	[ProductId] INT NOT NULL,
	[Quantity] INT NOT NULL,
	[PurchasePrice] Money not null,
	[Tax] Money not null default 0, 
    CONSTRAINT [FK_SaleDetail_ToSale] FOREIGN KEY ([SaleId]) REFERENCES [Sale]([Id]),
	CONSTRAINT [FK_SaleDetail_ToProduct] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id])
)
