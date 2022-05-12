namespace pearlbeachhotel.Models
{
    public class EmailModel
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public EmailModel()
        {
            Subject = "Customer contacts Pearl Beach Hotel";
        }
    }
    public class EmailInput
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
    }
}
