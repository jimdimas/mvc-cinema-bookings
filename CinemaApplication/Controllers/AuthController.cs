using BCrypt.Net;
using CinemaApplication.Data;
using CinemaApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            if (user == null)
            {
                TempData["message"] = "Something went wrong , try again";
                return View();
            }
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
            user.Password=BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.CreateTime = DateTime.Now;
            var serializedUser = JsonConvert.SerializeObject(user);
            if (user.Email.EndsWith("@cinema.com"))
            {
                ContentAdmin contentAdmin = JsonConvert.DeserializeObject<ContentAdmin>(serializedUser);
                contentAdmin.Role = "CONTENT-ADMIN";
                contentAdmin.ContentAdminId = Guid.NewGuid();
                _db.Users.Add(contentAdmin);
                _db.SaveChanges();

            } else if (user.Email.EndsWith("@cinema.admin.com"))
            {
                Admin admin = JsonConvert.DeserializeObject<Admin>(serializedUser);
                admin.Role = "ADMIN";
                admin.AdminId = Guid.NewGuid();
                _db.Users.Add(admin);
                _db.SaveChanges();
            } else
            {
                Customer customer = JsonConvert.DeserializeObject<Customer>(serializedUser);
                customer.Role = "CUSTOMER";
                customer.CustomerId = Guid.NewGuid();
                _db.Users.Add(customer);
                _db.SaveChanges();
            }
            return RedirectToAction("SignIn","Auth");
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
                HttpContext.Session.SetString("username", dbUser.Username);
                HttpContext.Session.SetString("role", dbUser.Role);
                TempData["role"]=dbUser.Role;   
                return RedirectToAction("Index", "Movie");
            }
        }
    }
}
