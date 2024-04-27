using System.ComponentModel.DataAnnotations;
namespace MeterProService.DTO
{
    public class EndTripDto
    {
        [Key]
        public int cabId { get; set; }

        public string endAddress { get; set; }

        public  DateTime endTime { get; set; }

        public double price { get; set; }

        public float miles { get; set; }
    }
}
