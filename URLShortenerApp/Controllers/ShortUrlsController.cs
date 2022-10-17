using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using URLShortenerApp.Helpers;
using URLShortenerApp.Models;
using URLShortenerApp.Services.Abstraction;

namespace URLShortenerApp.Controllers
{
    public class ShortUrlsController : Controller
    {
        private readonly IShortUrlService _service;

        public ShortUrlsController(IShortUrlService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return RedirectToAction(actionName: nameof(Create));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string originalUrl)
        {
            var shortUrl = new ShortUrlModel
            {
                OriginalUrl = originalUrl
            };

            TryValidateModel(shortUrl);
            if (ModelState.IsValid)
            {
                var result = await _service.Save(shortUrl);
                if (!result.Success)
                    ViewBag.error = result.ErrorMessage.Message;

                return RedirectToAction(actionName: nameof(Show), routeValues: new { id = shortUrl.Id });
            }

            return View(shortUrl);
        }

        public async Task<IActionResult> Show(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var shortUrl = await _service.GetById(id.Value);
            if (!shortUrl.Success || shortUrl.Data == null)
            {
                return NotFound();
            }

            ViewData["Path"] = ShortUrlHelper.Encode(shortUrl.Data.Id);

            return View(shortUrl.Data);
        }

        [HttpGet("/ShortUrls/RedirectTo/{path:required}", Name = "ShortUrls_RedirectTo")]
        public async Task<IActionResult> RedirectTo(string path)
        {
            if (path == null)
            {
                return NotFound();
            }

            var shortUrl = await _service.GetByPath(path);
            if (!shortUrl.Success || shortUrl.Data == null)
            {
                return NotFound();
            }

            return Redirect(shortUrl.Data.GeneratedUrl);
        }
    }
}
