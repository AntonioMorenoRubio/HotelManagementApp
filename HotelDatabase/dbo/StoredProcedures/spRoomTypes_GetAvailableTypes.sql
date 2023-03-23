CREATE PROCEDURE [dbo].[spRoomTypes_GetAvailableTypes]
	@startDate date,
	@endDate date
AS
begin
	set nocount on;

	select rtypes.Id, rtypes.Title, rtypes.Description, rtypes.Price from RoomTypes rtypes
	left join Rooms rooms
	on rooms.RoomTypeId = rtypes.Id
	left join Reservations reserves
	on reserves.RoomId != rooms.Id
	where (@startDate >= reserves.StartDate and @startDate < reserves.EndDate)
	or (@endDate > reserves.StartDate and @endDate < reserves.EndDate)
	group by rtypes.Id, rtypes.Title, rtypes.Description, rtypes.Price
end