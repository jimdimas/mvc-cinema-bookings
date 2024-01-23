using BCrypt.Net;
using CinemaApplication.Data;
using CinemaApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApplication.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AuthController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User user)
        {
            if (_db.Users.FirstOrDefault(u => u.Username == user.Username) != null)
            {
                TempData["message"] = "Username already exists";
                return View();
            }
            if (_db.Users.FirstOrDefault(u => u.Email == user.Email) != null)
            {
                TempData["message"] = "Email already exists";
                return View();
            }
            user.CreateTime=DateTime.Now;
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Role = "USER";
            _db.Users.Add(user);
            _db.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(User user)
        {
            User dbUser = _db.Users.FirstOrDefault(u => u.Username == user.Username);
            if (dbUser == null)
            {
                TempData["message"] = "Invalid credentials provided";
                return View();
            }
            else
            {
                if (!BCrypt.Net.BCrypt.Verify(user.Password, dbUser.Password))
                {
                    TempData["message"] = "Invalid credentials provided";
                    return View();
                }
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
