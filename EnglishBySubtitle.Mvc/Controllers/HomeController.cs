using EnglishBySubtitle.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EnglishBySubtitle.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult Aboutus()
        {
            return View("Index");
        }

        public IActionResult Services()
        {
            return View("Index");
        }

        public IActionResult Contacts()
        {
            return View("Index");
        }

        public ActionResult Login()
        {
            return View("Login");
        }

        public ActionResult Signup()
        {
            return View("Signup");
        }    

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}