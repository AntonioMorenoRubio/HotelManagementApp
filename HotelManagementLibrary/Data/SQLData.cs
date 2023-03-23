using HotelManagementLibrary.Interfaces;
using HotelManagementLibrary.Models;

namespace HotelManagementLibrary.Data
{
    public class SqlData : IDatabaseData
    {
        private readonly ISqlDataAccess db;
        private const string connectionStringName = "SQLServer";

        public SqlData(ISqlDataAccess db)
        {
            this.db = db;
        }

        public List<RoomTypeModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate)
        {
            return db.LoadData<RoomTypeModel, dynamic>(
                "dbo.spRoomTypes_GetAvailableTypes",
                new { startDate, endDate },
                "SQLServer",
                true);
        }

        public void BookGuest(string firstName,
                                 string lastName,
                                 DateTime startDate,
                                 DateTime endDate,
                                 int roomTypeId)
        {
            //Upsert Client
            ClientModel client = db.LoadData<ClientModel, dynamic>(
                "dbo.spClient_Insert",
                new { firstName, lastName },
                connectionStringName,
                true)
                .First();

            //Find first Room of Type Available for Dates Indicated
            List<RoomModel> availableRooms = db.LoadData<RoomModel, dynamic>(
            "dbo.spRooms_GetFirstRoomAvailableByTypeAndDate",
            new { startDate, endDate, roomTypeId },
            "SQLServer",
            true);

            RoomTypeModel roomType = db.LoadData<RoomTypeModel, dynamic>(
            "select * from dbo.RoomTypes where Id = @Id",
            new { Id = roomTypeId },
            "SQLServer",
            false).First();

            //Calculate Final Price
            decimal totalCost = endDate.Date.Subtract(startDate.Date).Days * roomType.Price;

            db.SaveData<ReservationModel, dynamic>(
                "dbo.spReservation_Insert",
                new
                {
                    clientId = client.Id,
                    roomId = availableRooms.First().Id,
                    startDate,
                    endDate,
                    totalCost
                },
                connectionStringName,
                true);

        }

        public List<ReservationFullModel> GetTodayBookings()
        {
            DateTime today = DateTime.Today.Date;

            return db.LoadData<ReservationFullModel, dynamic>("dbo.spReservations_GetTodayReservations",
                                                          new { today },
                                                          connectionStringName,
                                                          true);
        }

        public List<ReservationFullModel> GetTodayBookingsByLastName(string lastName)
        {
            DateTime today = DateTime.Today.Date;

            return db.LoadData<ReservationFullModel, dynamic>("dbo.spReservations_GetTodayReservationsByLastName",
                                                          new { lastName, today },
                                                          connectionStringName,
                                                          true);
        }

        public void CheckInClientInReservation(int reservationId)
        {
            db.SaveData<ReservationModel, dynamic>("dbo.spReservations_CheckInClient",
                                                   new { Id = reservationId },
                                                   connectionStringName,
                                                   true);
        }
    }
}
