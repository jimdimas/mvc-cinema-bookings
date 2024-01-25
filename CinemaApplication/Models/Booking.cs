using Microsoft.EntityFrameworkCore;

namespace CinemaApplication.Models
{
    [PrimaryKey(nameof(Id))]
    public class Booking
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Screening Screening { get; set; }
        public int Seats { get; set; }
    }
}
