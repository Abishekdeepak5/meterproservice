using System.ComponentModel.DataAnnotations;
namespace MeterProService.DTO
{
    public class TripHistoryDto
    {
        [Key]
        public int? id { get; set; }

        public double? price { get; set; }


        public int? userId { get; set; }

        public int? cabId { get; set; }
        public DateTime? startTime { get; set; }

        public DateTime? endTime { get; set; }
        public string? startLocation { get; set; }
        public string? endLocation { get; set; }
        public double? miles { get; set; }

    }
}
