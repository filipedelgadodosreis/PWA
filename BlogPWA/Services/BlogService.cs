using BlogPWA.Infrastructure;
using BlogPWA.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlogPWA.Services
{
    public class BlogService : IBlogService
    {
        private readonly HttpClient _httpClient;
        private readonly IHostingEnvironment _env;
        private readonly string _remoteServiceBaseUrl;

        public IOptions<AppSettings> Settings { get; }

        public BlogService(HttpClient httpClient, IOptions<AppSettings> settings, IHostingEnvironment env)
        {
            _env = env;
            Settings = settings;
            _httpClient = httpClient;

            _remoteServiceBaseUrl = $"{Settings.Value.BlogUrl}/api/blog/";
        }

        public async Task<BlogViewModel> GetPosts()
        {
            var uri = Api.Blog.GetLatestPosts(_remoteServiceBaseUrl);
            var responseString = await _httpClient.GetStringAsync(uri);

            var blogViewModel = JsonConvert.DeserializeObject<BlogViewModel>(responseString);


            return blogViewModel;
        }

        public async Task<BlogViewModel> GetOlderPosts(int oldestPostId)
        {
            var uri = Api.Blog.GetOlderPosts(_remoteServiceBaseUrl, oldestPostId);
            var responseString = await _httpClient.GetStringAsync(uri);

            var posts = JsonConvert.DeserializeObject<BlogViewModel>(responseString);

            return posts;
        }

        public async Task<string> GetPostText(string link)
        {
            var uri = Api.Blog.GetPostText(_remoteServiceBaseUrl);
            var responseString = await _httpClient.GetStringAsync(uri);

            var posts = JsonConvert.DeserializeObject<IEnumerable<BlogPost>>(responseString);

            var post = posts.FirstOrDefault(_ => _.Link == link);

            return string.IsNullOrEmpty(responseString) ? string.Empty : File.ReadAllText($"{_env.WebRootPath}/Posts/{post.PostId}_post.md");
        }

        public async Task<IEnumerable<BlogViewModel>> GetLatestPosts()
        {
            var uri = Api.Blog.GetLatestPosts(_remoteServiceBaseUrl);
            var responseString = await _httpClient.GetStringAsync(uri);

            var posts = JsonConvert.DeserializeObject<IEnumerable<BlogViewModel>>(responseString);

            return posts;
        }


    }
}
