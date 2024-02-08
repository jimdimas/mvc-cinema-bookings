using CinemaApplication.Data;
using CinemaApplication.Filters;
using CinemaApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApplication.Controllers
{
    [AuthenticationFilter]
    public class CinemaController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CinemaController(ApplicationDbContext db) {
            _db = db;
        }
        public IActionResult Index()
        {
            if (!HttpContext.Session.GetString("role").Equals("ADMIN")){
                return RedirectToAction("Index", "Movie");
            }
            TempData["role"] = HttpContext.Session.GetString("role");
            IEnumerable<Cinema> cinemas = _db.Cinemas;
            return View(cinemas);
        }

        [HttpPost]
        public IActionResult Create(String Name,int Seats,Boolean ThreeDimensions)
        {
            if (!HttpContext.Session.GetString("role").Equals("ADMIN"))
            {
                return RedirectToAction("Index", "Movie");
            }
            TempData["role"] = HttpContext.Session.GetString("role");
            Cinema cinema = new Cinema();
            cinema.Name = Name;
            cinema.Seats = Seats;
            cinema.ThreeDim = (ThreeDimensions) ? "Yes" : "No";
            _db.Cinemas.Add(cinema);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(String name)
        {
            if (!HttpContext.Session.GetString("role").Equals("ADMIN"))
            {
                return RedirectToAction("Index", "Movie");
            }
            TempData["role"] = HttpContext.Session.GetString("role");
            if (_db.Screenings.Include(s => s.Cinema).FirstOrDefault(s => s.Cinema.Name.Equals(name)) != null)
            {
                TempData["error"] = "Cannot delete given cinema entry , screenings on given cinema exist";
                return RedirectToAction("Index", "Cinema");
            }
            Cinema cinema = _db.Cinemas.Find(name);
            if (cinema != null)
            {
                _db.Remove(cinema);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
