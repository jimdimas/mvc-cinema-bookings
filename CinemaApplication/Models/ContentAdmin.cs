using Microsoft.EntityFrameworkCore;

namespace CinemaApplication.Models
{
    public class ContentAdmin:User
    {
        public Guid ContentAdminId { get; set; } 
    }
}
