using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeterProService.Models
{
    public class Trip 
    {
        [Key]
        public int? id { get;set; }

        public String? address { get; set; }

        public double? price { get; set; }

        
        public int? userId { get; set; }


        public User_detail? user { get; set; }

        [Column(TypeName ="decimal(12,9)")]
        public decimal? latitude { get; set; }

        [Column(TypeName = "decimal(12,9)")]
        public decimal? longitude { get; set; }

        public int?  cabId { get; set; }
       public DateTime? startTime { get; set; }


        public DateTime? endTime { get; set; }



        public string? startLocation { get; set; }
        public string? endLocation { get; set; }

        public byte[]? ImageData { get; set; }

        public double? miles { get; set; }

    }

}
