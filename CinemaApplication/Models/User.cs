using Microsoft.EntityFrameworkCore;

namespace CinemaApplication.Models
{
    [PrimaryKey(nameof(Username))]
    public class User
    {
        public String Username { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }    
        public DateTime CreateTime { get; set; }
        public String Role { get; set; }
    }
}
