using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using pearlbeachhotel.Models;
using System.Globalization;

namespace pearlbeachhotel.Controllers
{
    public class RoomController : Controller
    {
        private readonly ILogger<RoomController> _logger;
        public RoomController(ILogger<RoomController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {

            var model = new List<RoomsModel>();

                var date = DateTime.Now;
                var path = string.Empty;
                List<int> hight = new List<int> { 1, 2, 4, 5, 6, 7, 8, 9 };
                List<int> low = new List<int> { 3, 10, 11, 12 };
                List<string> hightDayOfWeek = new List<string> { "Thursday", "Friday", "Saturday" };
                List<string> lowDayOfWeek = new List<string> { "Sunday", "Monday", "Tuesday", "Wednesday" };
                if (hight.Contains(date.Month))
                {
                    if (hightDayOfWeek.Contains(date.DayOfWeek.ToString()))
                    {
                        path = "./wwwroot/data/roomHight12456789.csv";
                    }
                    else if (lowDayOfWeek.Contains(date.DayOfWeek.ToString()))
                    {
                        path = "./wwwroot/data/roomLow12456789.csv";
                    }
                }
                else if (low.Contains(date.Month))
                {
                    if (hightDayOfWeek.Contains(date.DayOfWeek.ToString()))
                    {
                        path = "./wwwroot/data/roomHight3101112.csv";
                    }
                    else if (lowDayOfWeek.Contains(date.DayOfWeek.ToString()))
                    {
                        path = "./wwwroot/data/roomLow3101112.csv";
                    }
                }

                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    model = csv.GetRecords<RoomsModel>().ToList();
                }           
            
            return View(model);
        }

    }
}