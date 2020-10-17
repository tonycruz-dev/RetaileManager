CREATE PROCEDURE spProduct_ById
	@Id int
AS
BEGIN

	SET NOCOUNT ON;

	SELECT [Id],[ProductName],[Discription],[RetailPrice],[QuantityInStock] ,[IsTaxable]  FROM [dbo].[Product]
	where [Id] = @Id
END
GO
