using Microsoft.EntityFrameworkCore;

namespace CinemaApplication.Models
{
    [PrimaryKey(nameof(Id))]
    public class Screening
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public Cinema Cinema { get; set; }
        public DateTime Time { get; set; }
        public ContentAdmin ContentAdmin { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        private int availableSeats;

        public int getAvailableSeats()
        {
            return availableSeats;
        }

        public void setAvailableSeats(int _availableSeats)
        {
            this.availableSeats = _availableSeats;
        }
    }
}
