using HotelManagementLibrary.Models;

namespace HotelManagementLibrary.Interfaces
{
    public interface IDatabaseData
    {
        void BookGuest(string firstName, string lastName, DateTime startDate, DateTime endDate, int roomTypeId);
        void CheckInClientInReservation(int reservationId);
        List<RoomTypeModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate);
        RoomTypeModel GetRoomTypeById(int roomTypeId);
        List<ReservationFullModel> GetTodayBookings();
        List<ReservationFullModel> GetTodayBookingsByLastName(string lastName);
    }
}