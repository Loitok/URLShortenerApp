using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using URLShortenerApp.Models;
using URLShortenerApp.Services.Abstraction;

namespace URLShortenerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _service;

        public HomeController(IHomeService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("idUser") != null)
            {
                return RedirectToAction(controllerName: "ShortUrls", actionName: "Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserMasterModel user)
        {
            if (ModelState.IsValid)
            {
                var check = await _service.FindByEmail(user.Email);

                if (check.Success && check.Data == null)
                {
                    var register = await _service.Register(user);
                    if (!register.Success)
                        ViewBag.error = register.ErrorMessage.Message;

                    return RedirectToAction(controllerName: "ShortUrls", actionName: "Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var data = await _service.FindByPassword(email, password);

                if (data.Success && data.Data.Any())
                {
                    HttpContext.Session.SetString("Name", data.Data.First().Name);
                    HttpContext.Session.SetString("Email", data.Data.First().Email);
                    HttpContext.Session.SetInt32("idUser", data.Data.First().Id);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Register");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

    }
}
