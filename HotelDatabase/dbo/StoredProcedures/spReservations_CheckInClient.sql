CREATE PROCEDURE [dbo].[spReservations_CheckInClient]
	@Id int
AS
	begin
		set nocount on;

		update dbo.Reservations
		set ClientHasCheckedIn = 1
		where Id = @Id;
	end