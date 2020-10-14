CREATE PROCEDURE [dbo].[spProduct_GetAll]

AS
	
    BEGIN
	SET NOCOUNT ON;


	SELECT [Id]
      ,[ProductName]
      ,[Discription]
      ,[RetailPrice]
      ,[CreatedDate]
      ,[LastModified]
  FROM [dbo].[Product]
  order by [ProductName]
END
	

