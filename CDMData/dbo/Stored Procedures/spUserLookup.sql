CREATE PROCEDURE [dbo].[spUserLookup]
	@id nvarchar(128)
AS
begin
   SELECT Id, FirstName, LastName, Email from [dbo].[User] where Id = @id
end
	
