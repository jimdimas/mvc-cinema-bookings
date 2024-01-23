using Microsoft.EntityFrameworkCore;

namespace CinemaApplication.Models
{
    public class Admin:User
    {
        public Guid AdminId { get; set; }   
    }
}
