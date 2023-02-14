using Microsoft.AspNetCore.Identity;

namespace Hotels.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        public string City { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
