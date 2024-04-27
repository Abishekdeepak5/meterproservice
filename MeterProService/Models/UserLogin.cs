using System.ComponentModel.DataAnnotations;

namespace MeterProService.Models
{
    public class UserLogin
    {
        
        public required string userName { get; set; }
        
        public required string password { get; set; }
    }
}
