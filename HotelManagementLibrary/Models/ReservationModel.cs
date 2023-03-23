using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementLibrary.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int RoomId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public bool ClientCheckedIn { get; set; }
        public decimal TotalCost { get; set; }
    }
}
