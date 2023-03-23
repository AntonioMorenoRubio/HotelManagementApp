CREATE PROCEDURE [dbo].[spReservations_GetTodayReservations]
	@today date
AS
begin
	set nocount on;

	select reserves.Id, c.FirstName, c.LastName, reserves.StartDate, reserves.EndDate, t.Title, r.RoomNumber, reserves.TotalCost, reserves.ClientHasCheckedIn
	from Reservations reserves
	inner join Clients c on c.Id = reserves.ClientId
	inner join Rooms r on reserves.RoomId = r.Id
	inner join RoomTypes t on r.RoomTypeId = t.Id
	where reserves.StartDate = @today
end