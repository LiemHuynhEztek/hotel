using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using pearlbeachhotel.Models;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Mail;

namespace pearlbeachhotel.Controllers
{
    public class HotelController : Controller
    {
        private readonly ILogger<HotelController> _logger;
        public HotelController(ILogger<HotelController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Room()
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
                    path = "./Data/roomHight12456789.csv";
                }
                else if (lowDayOfWeek.Contains(date.DayOfWeek.ToString()))
                {
                    path = "./Data/roomLow12456789.csv";
                }
            }
            else if (low.Contains(date.Month))
            {
                if (hightDayOfWeek.Contains(date.DayOfWeek.ToString()))
                {
                    path = "./Data/roomHight3101112.csv";
                }
                else if (lowDayOfWeek.Contains(date.DayOfWeek.ToString()))
                {
                    path = "./Data/roomLow3101112.csv";
                }
            }

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                model = csv.GetRecords<RoomsModel>().ToList();
            }
            return View(model);
        }

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        /*   public void SendEmail(string name, string email, string phoneNumber, string body)
           {

               var emailConfig = new EmailConfig();
               var emailModel = new EmailModel();

               emailModel.Body = $"Họ và tên: {name}\nSố điện thoại: {phoneNumber}\nEmail: {email}\nNội dung: {body}\n";
               using (var reader = new StreamReader("./Data/email.csv"))
               using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
               {
                   emailConfig = csv.GetRecords<EmailConfig>().FirstOrDefault();
               }
               emailModel.To = emailConfig.To;
               MailMessage msg = new MailMessage();

               msg.From = new MailAddress(emailConfig.Email);
               msg.To.Add(emailModel.To);
               msg.Subject = emailModel.Subject;
               msg.Body = emailModel.Body;


               using (SmtpClient client = new SmtpClient())
               {
                   client.EnableSsl = true;
                   client.UseDefaultCredentials = false;
                   client.Credentials = new NetworkCredential(emailConfig.Email, emailConfig.Password);
                   client.Host = emailConfig.Host;
                   client.Port = 587;
                   client.DeliveryMethod = SmtpDeliveryMethod.Network;

                   client.Send(msg);
               }
           }*/
        public IActionResult SlideImage()
        {
            return View();
        }

        public void SendEmail(string name, string email, string phoneNumber, string body)
        {
            var emailModel = new EmailModel();
            var emailConfig = new EmailConfig();
            using (var reader = new StreamReader("./wwwroot/data/email.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                emailConfig = csv.GetRecords<EmailConfig>().FirstOrDefault();
            }

            var fromAddress = new MailAddress(emailConfig.Email);
            var toAddress = new MailAddress(emailConfig.To);
            string fromPassword = emailConfig.Password;
            string subject = emailModel.Subject;
            string bodyEmail = $"Họ và tên: {name}\nSố điện thoại: {phoneNumber}\nEmail: {email}\nNội dung: {body}\n";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = bodyEmail
            })
            {
                smtp.Send(message);
            }
        }
    }
}