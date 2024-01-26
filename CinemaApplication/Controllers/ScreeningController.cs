using CinemaApplication.Data;
using CinemaApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using CinemaApplication.Filters;
namespace CinemaApplication.Controllers
{
    [AuthenticationFilter]
    public class ScreeningController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ScreeningController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Index(String MovieName)
        {
            TempData["MovieName"] = MovieName;
            TempData["role"] = HttpContext.Session.GetString("role");
            IEnumerable<Screening> movieScreenings = _db.Screenings
                .Where(s => s.Movie.MovieName == MovieName && s.Time > DateTime.Now)
                .Include(s => s.Cinema)
                .Include(s => s.Bookings)
                .OrderBy(s=>s.Time);
            foreach(var screening in movieScreenings)
            {
                screening.setAvailableSeats(screening.Cinema.Seats - screening.Bookings.Sum(b => b.Seats));
            }
            return View(movieScreenings);
        }
        [HttpPost]
        public IActionResult Create(String MovieName,String CinemaName,DateTime Time)
        {
            if (!HttpContext.Session.GetString("role").Equals("CONTENT-ADMIN"))
            {
                return RedirectToAction("Index", "Movie");
            }
            Cinema cinema = _db.Cinemas.FirstOrDefault(c=>c.Name == CinemaName);
            Movie movie = _db.Movies.FirstOrDefault(m=>m.MovieName==MovieName);
            ContentAdmin contentAdmin = _db.Set<ContentAdmin>().FirstOrDefault(u => u.Username.Equals(HttpContext.Session.GetString("username")));
            Screening screening = new Screening();
            screening.Movie = movie;
            screening.ContentAdmin = contentAdmin;
            screening.Cinema = cinema;
            screening.Time = Time;
            _db.Screenings.Add(screening);
            _db.SaveChanges();
            return RedirectToAction("Index", new {MovieName=MovieName});
        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (!HttpContext.Session.GetString("role").Equals("CONTENT-ADMIN"))
            {
                return RedirectToAction("Index", "Movie");
            }
            TempData["role"] = HttpContext.Session.GetString("role");
            Screening screening = _db.Screenings.Include(s => s.Movie).Include(s => s.Cinema).FirstOrDefault(s => s.Id == Id);
            return View(screening);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Screening screening,String MovieName)
        {
            if (!HttpContext.Session.GetString("role").Equals("CONTENT-ADMIN"))
            {
                return RedirectToAction("Index", "Movie");
            }
            TempData["role"] = HttpContext.Session.GetString("role");
            _db.Entry(screening).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index", "Screening",new {MovieName=MovieName});
        }

        public IActionResult Delete(int? Id)
        {
            if (!HttpContext.Session.GetString("role").Equals("CONTENT-ADMIN"))
            {
                return RedirectToAction("Index", "Movie");
            }
            TempData["role"] = HttpContext.Session.GetString("role");
            Screening screening = _db.Screenings.Include(s=>s.Movie).FirstOrDefault(s=> s.Id==Id);
            String movieName = screening.Movie.MovieName;
            _db.Screenings.Remove(screening);
            _db.SaveChanges();
            return RedirectToAction("Index", "Screening",new { MovieName = movieName });
        }
    }
}
