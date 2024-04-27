using System.ComponentModel.DataAnnotations;

namespace MeterProService.DTO
{
    public class StartAddressDto
    {
        [Key]
        public int cabId { get; set; }

        public string startAddress { get; set; }

        public DateTime startTime{ get; set; }

    }
}
