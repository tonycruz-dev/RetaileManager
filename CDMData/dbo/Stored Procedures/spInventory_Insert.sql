CREATE PROCEDURE [dbo].[spInventory_Insert]
   @Id int,
   @ProductId int ,
	@Quantity int ,
	@PurchasePrice money,
	@PurchaseDate datetime2
	
AS
BEGIN

	SET NOCOUNT ON;

	insert into [dbo].[Inventory]([ProductId],[Quantity],[PurchasePrice],[PurchaseDate])
	values(@ProductId, @Quantity , @PurchasePrice, @PurchaseDate)

END
