using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementLibrary.Models
{
    public class ReservationFullModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly startDate { get; set; }
        public DateOnly endDate { get; set; }
        public string Title { get; set; }
        public string RoomNumber { get; set; }
        public decimal TotalCost { get; set; }
        public bool ClientHasCheckedIn { get; set; }

    }
}
