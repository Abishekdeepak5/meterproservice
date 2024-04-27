using System.ComponentModel.DataAnnotations;

namespace MeterProService.DTO
{
    public class CabDto
    {
        [Key]
        public int cabId { get; set; }
        public string carNumber { get; set; }
    }
}
