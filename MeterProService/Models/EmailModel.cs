namespace WebApi.Models
{
    public class EmailModel
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string userId { get;set; }
        public IFormFile formFile { get; set; }
    }
}
 
