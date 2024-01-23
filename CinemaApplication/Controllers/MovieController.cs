using CinemaApplication.Data;
using CinemaApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApplication.Controllers
{
    public class MovieController : Controller
    {
        ApplicationDbContext _db;
        public MovieController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Movie> movies = _db.Movies;
            return View(movies);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            ContentAdmin contentAdmin = _db.Set<ContentAdmin>().FirstOrDefault(c => c.Username == "cadmin");
            movie.ContentAdmin = contentAdmin;
            _db.Movies.Add(movie);
            _db.SaveChanges();
            return RedirectToAction("Index","Movie");
        }
    }
}
