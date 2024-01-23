using Microsoft.EntityFrameworkCore;

namespace CinemaApplication.Models
{
    [PrimaryKey(nameof(MovieName))]
    public class Movie
    {
        public String MovieName { get; set; }
        public String MovieDescription { get; set; }
        public int Length { get; set; }
        public String Genre { get; set; }
        public String Director { get; set; }
        public ContentAdmin ContentAdmin { get; set; }
    }
}
