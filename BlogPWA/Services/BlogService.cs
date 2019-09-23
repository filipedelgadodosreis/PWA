using BlogPWA.Infrastructure;
using BlogPWA.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
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
        private readonly IOptions<AppSettings> _settings;

        public BlogService(HttpClient httpClient, IOptions<AppSettings> settings, IHostingEnvironment env)
        {
            _env = env;
            _settings = settings;
            _httpClient = httpClient;

            _remoteServiceBaseUrl = $"{_settings.Value.BlogUrl}/api/blog/";
        }

        public async Task<BlogViewModel> GetLatestPosts()
        {
            var uri = Api.Blog.GetLatestPosts(_remoteServiceBaseUrl);
            var responseString = await _httpClient.GetStringAsync(uri);

            var blogViewModel = JsonConvert.DeserializeObject<BlogViewModel>(responseString);


            return blogViewModel;
        }

        public List<BlogViewModel> GetOlderPosts(int oldestPostId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetPostText(string link)
        {
            var uri = Api.Blog.GetPostsLinks(_remoteServiceBaseUrl);
            var responseString = await _httpClient.GetStringAsync(uri);

            var posts = JsonConvert.DeserializeObject<IEnumerable<BlogPost>>(responseString);

            var post = posts.FirstOrDefault(_ => _.Link == link);

            return string.IsNullOrEmpty(responseString) ? string.Empty : File.ReadAllText($"{_env.WebRootPath}/Posts/{post.PostId}_post.md");
        }
    }
}
