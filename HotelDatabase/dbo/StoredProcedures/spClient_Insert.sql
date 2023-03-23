CREATE PROCEDURE [dbo].[spClient_Insert]
	@firstName nvarchar(50),
	@lastName nvarchar(100)
AS
begin
	set nocount on;

	IF NOT EXISTS (SELECT 1 from dbo.Clients c where c.FirstName = @firstName and c.LastName = @lastName)
	begin
		INSERT INTO Clients (FirstName, LastName)
		Values (@firstName, @lastName);
	end

	select top 1 * from dbo.Clients c
	where c.FirstName = @firstName and c.LastName = @lastName;
end
