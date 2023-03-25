using HotelLibrary.Interfaces;
using HotelLibrary.Models;

namespace HotelLibrary.Data
{
    public class SqliteData : IDatabaseData
    {
        private readonly ISqliteDataAccess db;
        private const string connectionStringName = "SQLite";

        public SqliteData(ISqliteDataAccess db)
        {
            this.db = db;
        }

        public List<RoomTypeModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate)
        {
            List<RoomTypeModel> output = new();
            output = db.LoadData<RoomTypeModel, dynamic>(
                @"select rtypes.Id, rtypes.Title, rtypes.Description, rtypes.Price from RoomTypes rtypes
						inner join Rooms rooms
						on rooms.RoomTypeId = rtypes.Id
						where rooms.Id not in (
						select reserves.RoomId from Reservations reserves
							where (@startDate < reserves.StartDate and @endDate > reserves.EndDate)
							 or (reserves.StartDate <= @endDate and @endDate < reserves.EndDate)
							 or (reserves.StartDate <= @startDate and @startDate < reserves.StartDate)
						)
						group by rtypes.Id, rtypes.Title, rtypes.Description, rtypes.Price;",
                new { startDate, endDate },
                connectionStringName);

            output.ForEach(x => x.Price /= 100);
            return output;
        }

        public void BookGuest(string firstName, string lastName, DateTime startDate, DateTime endDate, int roomTypeId)
        {
            //Upsert Client
            int results = db.LoadData<ClientModel, dynamic>(
                @"SELECT 1 from Clients c where c.FirstName = @firstName and c.LastName = @lastName",
                new { firstName, lastName },
                connectionStringName).Count;

            if (results == 0)
            {
                db.SaveData<ClientModel, dynamic>(@"INSERT INTO Clients (FirstName, LastName)
							Values (@firstName, @lastName);",
                            new { firstName, lastName },
                            connectionStringName);
            }
            ClientModel client = db.LoadData<ClientModel, dynamic>
                (@"SELECT c.Id, c.FirstName, c.LastName from Clients c 
						where c.FirstName =	 @firstName 
						and c.LastName = @lastName
						LIMIT 1;",
                       new { firstName, lastName },
                       connectionStringName).First();

            //Find first Room of Type Available for Dates Indicated
            List<RoomModel> availableRooms = db.LoadData<RoomModel, dynamic>(
                @"select r.* from Rooms r
					  inner join RoomTypes t on t.Id = r.RoomTypeId
					  where r.RoomTypeId = @roomTypeId
					  and r.Id not in (
						  select reserves.RoomId from Reservations reserves
						  where (@startDate >= reserves.StartDate and @startDate < reserves.EndDate)
						  or (@endDate > reserves.StartDate and @endDate < reserves.EndDate)
					  )",
            new { startDate, endDate, roomTypeId },
            connectionStringName);

            RoomTypeModel roomType = db.LoadData<RoomTypeModel, dynamic>(
            "select * from RoomTypes where Id = @Id",
            new { Id = roomTypeId },
            connectionStringName).First();

            //Calculate Final Price
            decimal totalCost = endDate.Date.Subtract(startDate.Date).Days * roomType.Price;

            db.SaveData<ReservationModel, dynamic>(
                @"insert into Reservations (ClientId, RoomId, StartDate, EndDate, TotalCost)
						values (@clientId, @roomId, @startDate, @endDate, @totalCost);",
                new
                {
                    clientId = client.Id,
                    roomId = availableRooms.First().Id,
                    startDate,
                    endDate,
                    totalCost
                },
                connectionStringName);

        }

        public List<ReservationFullModel> GetTodayBookings()
        {
            DateTime today = DateTime.Today.Date;

            return db.LoadData<ReservationFullModel, dynamic>(
                @"select reserves.Id, c.FirstName, c.LastName, reserves.StartDate, reserves.EndDate, t.Title, r.RoomNumber, reserves.TotalCost, reserves.ClientHasCheckedIn
	                    from Reservations reserves
	                    inner join Clients c on c.Id = reserves.ClientId
	                    inner join Rooms r on reserves.RoomId = r.Id
	                    inner join RoomTypes t on r.RoomTypeId = t.Id
	                    where reserves.StartDate = @today",
                        new { today },
                        connectionStringName);
        }

        public List<ReservationFullModel> GetTodayBookingsByLastName(string lastName)
        {
            DateTime today = DateTime.Today.Date;

            return db.LoadData<ReservationFullModel, dynamic>(
                @"select reserves.Id, c.FirstName, c.LastName, reserves.StartDate, reserves.EndDate, t.Title, r.RoomNumber, reserves.TotalCost, reserves.ClientHasCheckedIn
	                    from Reservations reserves
	                    inner join Clients c on c.Id = reserves.ClientId
	                    inner join Rooms r on reserves.RoomId = r.Id
	                    inner join RoomTypes t on r.RoomTypeId = t.Id
	                    where reserves.StartDate = @today and c.LastName = @lastName",
                      new { lastName, today },
                      connectionStringName);
        }

        public void CheckInClientInReservation(int reservationId)
        {
            db.SaveData<ReservationModel, dynamic>(
                @"update Reservations
	                    set ClientHasCheckedIn = 1
	                    where Id = @id;",
                    new { id = reservationId },
                    connectionStringName);
        }

        public RoomTypeModel GetRoomTypeById(int roomTypeId)
        {
            return db.LoadData<RoomTypeModel, dynamic>(@"select * from RoomTypes rt
															where rt.Id = @id;",
                                                       new { id = roomTypeId },
                                                       connectionStringName).FirstOrDefault();
        }
    }
}
