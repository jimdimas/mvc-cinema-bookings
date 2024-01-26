using CinemaApplication.Data;
using CinemaApplication.Filters;
using CinemaApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CinemaApplication.Controllers
{
    [AuthenticationFilter]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<ContentAdmin> contentAdmins = _db.Set<ContentAdmin>();
            return View(contentAdmins);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
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
            if (!user.Email.EndsWith("@cinema.com"))
            {
                TempData["message"] = "Content Admin Email must end with @cinema.com";
                return View();
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.CreateTime = DateTime.Now;
            var serializedUser = JsonConvert.SerializeObject(user);
            ContentAdmin contentAdmin = JsonConvert.DeserializeObject<ContentAdmin>(serializedUser);
            contentAdmin.Role = "CONTENT-ADMIN";
            contentAdmin.ContentAdminId = Guid.NewGuid();
            _db.Users.Add(contentAdmin);
            _db.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public IActionResult Edit(String username)
        {
            ContentAdmin contentAdmin = _db.Set<ContentAdmin>().Find(username);
            return View(contentAdmin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ContentAdmin contentAdmin)
        {
            if (contentAdmin != null)
            {
                _db.Attach(contentAdmin);
                _db.Entry(contentAdmin).Property(c => c.CreateTime).IsModified = false;
                _db.Entry(contentAdmin).Property(c => c.Password).IsModified = false;
                _db.Entry(contentAdmin).Property(c => c.Role).IsModified = false;
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public IActionResult Delete(String username)
        {
            ContentAdmin contentAdmin = _db.Set<ContentAdmin>().Find(username);
            _db.Set<ContentAdmin>().Remove(contentAdmin);
            _db.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }

    }
}
