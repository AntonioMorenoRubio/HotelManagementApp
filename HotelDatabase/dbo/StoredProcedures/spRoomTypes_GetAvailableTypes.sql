CREATE PROCEDURE [dbo].[spRoomTypes_GetAvailableTypes]
	@startDate date,
	@endDate date
AS
begin
	set nocount on;

	select rtypes.Id, rtypes.Title, rtypes.Description, rtypes.Price from RoomTypes rtypes
	inner join Rooms rooms
	on rooms.RoomTypeId = rtypes.Id
	where rooms.Id not in (
	select reserves.RoomId from Reservations reserves
	where (@startDate < reserves.StartDate and @endDate > reserves.EndDate)
		or (reserves.StartDate <= @endDate and @endDate < reserves.EndDate)
		or (reserves.StartDate <= @startDate and @startDate < reserves.StartDate)
	)
	group by rtypes.Id, rtypes.Title, rtypes.Description, rtypes.Price;
end