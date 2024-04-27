using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MeterProService.Models
{
    public class UserDetail
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
    }
}
