using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using pearlbeachhotel.Models;
using System.Globalization;

namespace pearlbeachhotel.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        public ContactController(ILogger<ContactController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

    }
}
