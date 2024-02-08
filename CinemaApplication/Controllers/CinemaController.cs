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

        [HttpPost]
        public IActionResult Create(String Name,int Seats,Boolean ThreeDimensions)
        {
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
