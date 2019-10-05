using BlogPWA.Models;
using BlogPWA.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
            var posts = _blogSvc.GetPosts();
            return Json(posts);
        }

        public async Task<ContentResult> Post(string link)
        {
            return Content(await _blogSvc.GetPostText(link));
        }

        public JsonResult MoreBlogPosts(int oldestBlogPostId)
        {
            var posts = _blogSvc.GetOlderPosts(oldestBlogPostId);

            return Json(posts);
        }

    }
}
