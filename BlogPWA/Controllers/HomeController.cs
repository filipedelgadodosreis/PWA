using BlogPWA.Models;
using BlogPWA.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using Lib.Net.Http.WebPush;
using BlogPWA.Store;

namespace BlogPWA.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogService _blogSvc;
        private readonly IPushSubscriptionStore _subscriptionStore;
        private readonly PushServiceClient _pushClient;


        public HomeController(IBlogService blogSvc) => _blogSvc = blogSvc;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("publickey")]
        public ContentResult GetPublicKey()
        {
            return Content(_pushClient.DefaultAuthentication.PublicKey, "text/plain");
        }

        [HttpPost("notifications")]
        public async Task<IActionResult> SendNotification([FromBody]PushMessageViewModel messageVM)
        {
            var message = new PushMessage(messageVM.Notification)
            {
                Topic = messageVM.Topic,
                Urgency = messageVM.Urgency
            };

            await _subscriptionStore.ForEachSubscriptionAsync((PushSubscription subscription) =>
            {
                _pushClient.RequestPushMessageDeliveryAsync(subscription, message);
            });

            return NoContent();
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
