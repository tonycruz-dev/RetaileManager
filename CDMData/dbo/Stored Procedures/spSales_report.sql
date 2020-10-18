CREATE PROCEDURE [dbo].[spSales_report]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT s.[SaleDate]
      ,s.[SubTotal]
      ,s.[Tax]
      ,s.[Total]
	  ,u.FirstName
	  ,u.LastName
	  ,u.Email
  FROM [dbo].[Sale] s
  inner join [dbo].[User] u on s.CashierId = u.Id 

END
