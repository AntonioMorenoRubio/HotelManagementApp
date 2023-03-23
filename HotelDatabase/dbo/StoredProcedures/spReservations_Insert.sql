CREATE PROCEDURE [dbo].[spReservations_Insert]
	@clientId int,
	@roomId int,
	@startDate date,
	@endDate date,
	@totalCost money
AS
begin
	set nocount on;

	insert into Reservations (ClientId, RoomId, StartDate, EndDate, TotalCost)
	values (@clientId, @roomId, @startDate, @endDate, @totalCost);
end