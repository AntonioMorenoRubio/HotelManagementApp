CREATE PROCEDURE [dbo].[spReservations_CheckInClient]
	@id int
AS
	begin
		set nocount on;

		update dbo.Reservations
		set ClientHasCheckedIn = 1
		where Id = @id;
	end