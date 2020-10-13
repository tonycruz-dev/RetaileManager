CREATE PROCEDURE [dbo].[spProduct_GetAll]
AS
begin
set nocount on; 
     SELECT [Id]
          ,[ProductName]
          ,[Discription]
          ,[RetailPrice]
          ,[QuantityInStock]
          ,[CreatedDate]
          ,[LastModified]
       FROM [dbo].[Product]
       order by ProductName
end


