namespace WebApi.Models
{
    public class EmailTemplateModel
    {
        public string SenderEmail { get; set; }
        public string ReceiverEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
