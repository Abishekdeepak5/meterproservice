using System.ComponentModel.DataAnnotations;

namespace MeterProService.Models
{
    public class User_detail
    {
        [Key]
        public int id { get; set; }

        [MaxLength(50)] 

        public string? user_name { get; set; }
        [MaxLength(50)] 
        public string? last_name { get; set; }

        [MaxLength(50)] 
        public string? password { get; set; }

        [MaxLength(50)] 
        public string? first_name { get; set; }

        [MaxLength(50)] 
        public string? role { get; set; }

        public string? email { get; set; }


        public ICollection<Trip> trips;


        
        
    }
}



