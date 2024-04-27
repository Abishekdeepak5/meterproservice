namespace MeterProService.DTO
{
    public class TripImageDto
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public IFormFile formFile { get; set; }
    }
}
