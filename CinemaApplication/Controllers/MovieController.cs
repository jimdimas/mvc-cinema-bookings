using CinemaApplication.Data;
using CinemaApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CinemaApplication.Filters;
namespace CinemaApplication.Controllers
{
    [AuthenticationFilter]
    public class MovieController : Controller
    {
        ApplicationDbContext _db;
        public MovieController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            TempData["role"] = HttpContext.Session.GetString("role");
            IEnumerable<Movie> movies = _db.Movies;
            return View(movies);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!HttpContext.Session.GetString("role").Equals("CONTENT-ADMIN"))
            {
                return RedirectToAction("Index", "Movie");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            if (!HttpContext.Session.GetString("role").Equals("CONTENT-ADMIN"))
            {
                return RedirectToAction("Index", "Movie");
            }
            ContentAdmin contentAdmin = _db.Set<ContentAdmin>().FirstOrDefault(c => c.Username == HttpContext.Session.GetString("username"));
            movie.ContentAdmin = contentAdmin;
            _db.Movies.Add(movie);
            _db.SaveChanges();
            return RedirectToAction("Index","Movie");
        }
    }
}
