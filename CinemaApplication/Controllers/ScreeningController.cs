using CinemaApplication.Data;
using CinemaApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApplication.Controllers
{
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
            IEnumerable<Screening> movieScreenings = _db.Screenings.Where(s => s.Movie.MovieName == MovieName && s.Time>DateTime.Now).Include(s=> s.Cinema);
            return View(movieScreenings);
        }
        [HttpPost]
        public IActionResult Create(String MovieName,String CinemaName,DateTime Time)
        {
            Cinema cinema = _db.Cinemas.FirstOrDefault(c=>c.Name == CinemaName);
            Movie movie = _db.Movies.FirstOrDefault(m=>m.MovieName==MovieName);
            ContentAdmin contentAdmin = _db.Set<ContentAdmin>().FirstOrDefault(u => u.Username.Equals("cadmin"));
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
            Screening screening = _db.Screenings.Include(s => s.Movie).Include(s => s.Cinema).FirstOrDefault(s => s.Id == Id);
            return View(screening);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Screening screening,String MovieName)
        {
            _db.Entry(screening).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index", "Screening",new {MovieName=MovieName});
           
        }

        public IActionResult Delete(int? Id)
        {
            Screening screening = _db.Screenings.Include(s=>s.Movie).FirstOrDefault(s=> s.Id==Id);
            String movieName = screening.Movie.MovieName;
            _db.Screenings.Remove(screening);
            _db.SaveChanges();
            return RedirectToAction("Index", "Screening",new { MovieName = movieName });
        }
    }
}
