using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CinemaApplication.Data;
using CinemaApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaApplication.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookingController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            TempData["role"] = HttpContext.Session.GetString("role");
            IEnumerable<Booking> Bookings = _db.Bookings
                .Where(b => b.Customer.Username.Equals(HttpContext.Session.GetString("username")))
                .Include(b=>b.Screening)
                .ThenInclude(s=>s.Cinema)
                .Include(b=>b.Screening)
                .ThenInclude(s=>s.Movie)
                .OrderBy(b=>b.Screening.Time);
            return View(Bookings);
        }

        public IActionResult Create(int ScreeningId,int Seats)
        {
            TempData["role"] = HttpContext.Session.GetString("role");
            Screening screening = _db.Screenings.Find(ScreeningId);
            Customer customer = _db.Set<Customer>().Find(HttpContext.Session.GetString("username"));
            Booking booking = new Booking();
            booking.Screening = screening;
            booking.Customer = customer;
            booking.Seats = Seats;
            _db.Bookings.Add(booking);
            _db.SaveChanges();
            return RedirectToAction("Index", "Booking");
        }

        [HttpPost]
        public IActionResult Delete(int BookingId)
        {
            TempData["role"] = HttpContext.Session.GetString("role");
            Booking booking = _db.Bookings.Find(BookingId);
            if (booking != null)
            {
                _db.Bookings.Remove(booking);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "Booking");
        }
    }
}
