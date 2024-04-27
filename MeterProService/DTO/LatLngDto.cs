using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeterProService.DTO
{
    public class LatLngDto
    {
        [Key]
        public int id { get; set; }

        [Column(TypeName = "decimal(12,9)")]
        public decimal latitude { get; set; }

        [Column(TypeName = "decimal(12,9)")]
        public decimal longitude { get; set; }
    }
}
