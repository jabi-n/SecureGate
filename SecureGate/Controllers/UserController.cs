using Microsoft.AspNetCore.Mvc;
using SecureGate.Data;
using SecureGate.Models;
using Microsoft.AspNetCore.Http;

namespace SecureGate.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /User/Create
        [HttpPost]
        public IActionResult Create(User usr)
        {
            _context.Add(usr);
            _context.SaveChanges();
            return RedirectToAction("Landing");
        }

        // GET: /User/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /User/Login
        [HttpPost]
        public IActionResult Login(User model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
                if (user != null)
                {
                    HttpContext.Session.SetString("Username", user.Username);
                    return RedirectToAction("Landing");
                }
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
            }
            return View(model);
        }

        // GET: /User/Landing
        [HttpGet]
        public IActionResult Landing()
        {
            return View();
        }

        // POST: /User/Logout
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Index", "Home");
        }
    }
}
