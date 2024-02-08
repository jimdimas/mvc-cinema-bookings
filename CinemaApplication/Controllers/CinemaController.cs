using CinemaApplication.Data;
using CinemaApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApplication.Controllers
{
    public class CinemaController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CinemaController(ApplicationDbContext db) {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Cinema> cinemas = _db.Cinemas;
            return View(cinemas);
        }
    }
}
