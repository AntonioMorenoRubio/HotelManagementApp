using System.ComponentModel.DataAnnotations;
using HotelLibrary.Data;
using HotelLibrary.Databases;
using HotelLibrary.Interfaces;
using HotelLibrary.Models;
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

        [BindProperty(SupportsGet = true)]
        public bool SearchEnabled { get; set; } = false;

        [BindProperty]
        public List<RoomTypeModel> AvailableRoomTypes { get; set; }

        

        public RoomSearchModel(IDatabaseData db)
        {
            this.db = db;
        }

        public void OnGet()
        {
            if (SearchEnabled)
                AvailableRoomTypes = db.GetAvailableRoomTypes(StartDate, EndDate);
        }

        public IActionResult OnPost()
        {
            return RedirectToPage(new
            {
                SearchEnabled = true,
                StartDate = StartDate.ToString("yyyy-MM-dd"),
                EndDate = EndDate.ToString("yyyy-MM-dd")
            });
        }

    }
}
