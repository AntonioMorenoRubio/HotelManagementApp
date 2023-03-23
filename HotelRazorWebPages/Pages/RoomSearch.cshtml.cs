using System.ComponentModel.DataAnnotations;
using HotelManagementLibrary.Data;
using HotelManagementLibrary.Databases;
using HotelManagementLibrary.Interfaces;
using HotelManagementLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelApp.Web.Pages
{

    public class RoomSearchModel : PageModel
    {
        private readonly IDatabaseData db;

        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);
        [BindProperty]
        public List<RoomTypeModel> AvailableRoomTypes { get; set; }

        public RoomSearchModel(IDatabaseData db)
        {
            this.db = db;
        }

        public void OnGet()
        {
            AvailableRoomTypes = db.GetAvailableRoomTypes(StartDate, EndDate);
        }

        public IActionResult OnPost()
        {
            return RedirectToPage(new
            {
                StartDate,
                EndDate
            });
        }
    }
}
