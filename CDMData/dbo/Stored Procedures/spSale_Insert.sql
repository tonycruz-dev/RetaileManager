CREATE PROCEDURE dbo.spSale_Insert 
	 @Id int output
    ,@CashierId nvarchar(128)
    ,@SaleDate dateTime2
    ,@SubTotal money
    ,@Tax money
    ,@Total money
AS
BEGIN

	SET NOCOUNT ON;

insert into [dbo].[Sale]([CashierId],[SaleDate],[SubTotal],[Tax],[Total])
  values(@CashierId,@SaleDate,@SubTotal,@Tax,@Total)
  select @id = @@IDENTITY;

END
GO
