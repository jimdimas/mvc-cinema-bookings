using Microsoft.EntityFrameworkCore;

namespace CinemaApplication.Models
{
    [PrimaryKey(nameof(Name))]
    public class Cinema
    {
        public String Name { get; set; }
        public int Seats { get; set; }  
        public String ThreeDim { get; set; }
        public ICollection<Screening> Screenings { get; set; }
    }
}
