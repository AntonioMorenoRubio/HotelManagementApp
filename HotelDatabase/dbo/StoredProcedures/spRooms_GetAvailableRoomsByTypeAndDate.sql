CREATE PROCEDURE [dbo].[spRooms_GetAvailableRoomsByTypeAndDate]
	@startDate date,
	@endDate date,
	@roomTypeId int
AS
begin
	set nocount on;

	select r.* from Rooms r
	inner join RoomTypes t on t.Id = r.RoomTypeId
	where r.RoomTypeId = @roomTypeId
	and r.Id not in (
		select reserves.RoomId from Reservations reserves
		where (@startDate >= reserves.StartDate and @startDate < reserves.EndDate)
		or (@endDate > reserves.StartDate and @endDate < reserves.EndDate)
	)
end
