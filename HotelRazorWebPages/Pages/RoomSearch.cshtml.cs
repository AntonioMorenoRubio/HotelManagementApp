using HotelManagementLibrary.Data;
using HotelManagementLibrary.Databases;
using HotelManagementLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelApp.Web.Pages
{

    public class RoomSearchModel : PageModel
    {
        [BindProperty]
        public DateTime startDate { get; set; }
        [BindProperty]
        public DateTime endDate { get; set; }
        [BindProperty]
        public List<RoomTypeModel> availableRoomTypes { get; set; } 

        public void OnGet()
        {
            availableRoomTypes = new List<RoomTypeModel>();
            startDate = DateTime.Today;
            endDate = DateTime.Today;
        }

        void SearchAvailableRoomTypesByDate()
        {
            SqlData sqlData = new SqlData(new SqlServerDataAccess());
            availableRoomTypes = sqlData.GetAvailableRoomTypes(startDate, endDate);
        }

        void UpdateData()
        {
            if (availableRoomTypes.Count > 0)
            {

            }
        }
    }
}
