using Microsoft.EntityFrameworkCore;

namespace CinemaApplication.Models
{
    public class Customer:User
    {
        public Guid CustomerId { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
