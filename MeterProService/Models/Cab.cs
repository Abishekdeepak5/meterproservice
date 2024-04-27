using System.ComponentModel.DataAnnotations;

namespace MeterProService.Models
{
    public class Cab
    {
        [Key]
        public int id { get; set; } 

        [Required]
        [MaxLength(50)]
        public string? carNumber { get; set; }

        [MaxLength(50)]
        public string? make { get; set; }

        [MaxLength(50)]
        public string? model { get; set; }
 
        public int year { get; set; }

        public bool? IsAvailable { get; set; }
    }
}

