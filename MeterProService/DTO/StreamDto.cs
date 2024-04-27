using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MeterProService.DTO
{
    public class StreamDto
    {
        [Key]
        public int id { get; set; }

        [Column(TypeName = "decimal(12,9)")]
        public decimal latitude { get; set; }

        [Column(TypeName = "decimal(12,9)")]
        public decimal longitude { get; set; }
    }
}
