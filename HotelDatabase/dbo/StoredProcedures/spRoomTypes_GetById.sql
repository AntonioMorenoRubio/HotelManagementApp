CREATE PROCEDURE [dbo].[spRoomTypes_GetById]
	@id int
AS

begin
	set nocount on;

	select * from RoomTypes rt
	where rt.Id = @id;
end
