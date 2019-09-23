using BlogPWA.Models;
using BlogPWA.Services;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogPWA.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogService _blogSvc;

        public HomeController(IBlogService blogSvc) => _blogSvc = blogSvc;

        public IActionResult Index()
        {
            return View();
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

        public JsonResult LatestBlogPosts()
        {
            var posts = _blogSvc.GetLatestPosts();
            return Json(posts);
        }

        public ContentResult Post(string link)
        {
            var result = _blogSvc.GetPostText(link);

            return Content(result.ToString());
        }
    }
}
