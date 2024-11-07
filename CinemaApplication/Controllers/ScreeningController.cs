using CinemaApplication.Data;
using CinemaApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using CinemaApplication.Filters;
using Microsoft.IdentityModel.Tokens;
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
        public IActionResult IndexByDate(DateTime minTime, DateTime maxTime)
        {
            IEnumerable<Screening> screenings = _db.Screenings.Where(s => s.Time >= minTime && s.Time <= maxTime);
            return View(screenings);
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
                .OrderBy(s => s.Time);

            // booking available seats are assigned cinema seats minus the amount of bookings* seats per booking
            foreach (var screening in movieScreenings)
            {
                screening.setAvailableSeats(screening.Cinema.Seats - screening.Bookings.Sum(b => b.Seats));
            }
            return View(movieScreenings);
        }
        [HttpPost]
        [ContentAdminFilter]
        public IActionResult Create(String MovieName, String CinemaName, DateTime Time)
        {
            TempData["role"] = HttpContext.Session.GetString("role");
            Cinema cinema = _db.Cinemas.Find(CinemaName);
            if (cinema == null)
            {
                TempData["error"] = "Cinema does not exist";
                return RedirectToAction("Index", new { MovieName = MovieName });
            }
            Movie movie = _db.Movies.Find(MovieName);
            if (movie == null)
            {
                TempData["error"] = "Movie does not exist";
                return RedirectToAction("Index", "Movie");
            }
            
            if (!checkScheduleOverlaps(movie, cinema, Time))
            {
                TempData["error"] = "Cannot create screening for movie in given cinema at given time , overlaping schedule with other screening";
                return RedirectToAction("Index", new { MovieName = MovieName });
            }

            ContentAdmin contentAdmin = _db.Set<ContentAdmin>().Find(HttpContext.Session.GetString("username"));
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
        [ContentAdminFilter]
        public IActionResult Edit(int? Id)
        {
            TempData["role"] = HttpContext.Session.GetString("role");
            Screening screening = _db.Screenings.Include(s => s.Movie).Include(s => s.Cinema).FirstOrDefault(s => s.Id == Id);
            return View(screening);
        }

        [HttpPost]
        [ContentAdminFilter]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Screening updatedScreening)
        {
            TempData["role"] = HttpContext.Session.GetString("role");
            updatedScreening = _db.Screenings.Include(s => s.Movie).Include(s=>s.Cinema).FirstOrDefault(s=>s.Id==updatedScreening.Id);
            if (updatedScreening == null) 
            {
                TempData["error"] ="Screening not found , try again";
                return RedirectToAction("Index", new { MovieName = updatedScreening.Movie.MovieName });
            }
            if (!checkScheduleOverlaps(updatedScreening.Movie, updatedScreening.Cinema, updatedScreening.Time))  //check cinema,movie from db field 
            {
                TempData["error"] = "Cannot create screening for movie in given cinema at given time , overlaping schedule with other screening";
                return RedirectToAction("Index", new { MovieName = updatedScreening.Movie.MovieName });
            }
            _db.Entry(updatedScreening).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index", "Screening",new {MovieName= updatedScreening.Movie.MovieName });
        }

        [ContentAdminFilter]
        public IActionResult Delete(int? Id)
        {
            TempData["role"] = HttpContext.Session.GetString("role");
            Screening screening = _db.Screenings.Include(s => s.Bookings).Include(s => s.Movie).FirstOrDefault(s => s.Id == Id);
            if (screening == null)
            {
                TempData["error"] = "Screening does not exist , try again";
                return RedirectToAction("Index", "Movie");
            }
            _db.Bookings.RemoveRange(screening.Bookings);
            String movieName = screening.Movie.MovieName;
            _db.Screenings.Remove(screening);
            _db.SaveChanges();
            return RedirectToAction("Index", "Screening",new { MovieName = movieName });
        }

        private Boolean checkScheduleOverlaps(Movie movie,Cinema cinema,DateTime Time)
        {
            if (_db.Screenings.First() != null &&
                (!_db.Screenings
                .Include(s => s.Movie)
                .Include(s => s.Cinema)
                .Where(
                    s => (DateTime.Compare(s.Time.AddMinutes(s.Movie.Length), Time) >= 0
                    && DateTime.Compare(Time.AddMinutes(movie.Length), s.Time) >= 0)
                    && s.Cinema.Name.Equals(cinema.Name))
                .IsNullOrEmpty()))   //checks whether this screening's time and duration overlap with existing one 
            {
                return false;
            }
            return true;
        }
    }
}
