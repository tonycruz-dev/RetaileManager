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
	DECLARE @InsertedRows AS TABLE (Id int);
    insert into [dbo].[Sale]([CashierId],[SaleDate],[SubTotal],[Tax],[Total]) OUTPUT Inserted.Id INTO @InsertedRows
    values(@CashierId,@SaleDate,@SubTotal,@Tax,@Total)
 
    SELECT Id FROM @InsertedRows
END
