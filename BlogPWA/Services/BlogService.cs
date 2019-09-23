using BlogPWA.Infrastructure;
using BlogPWA.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlogPWA.Services
{
    public class BlogService : IBlogService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;
        private readonly IOptions<AppSettings> _settings;

        public BlogService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
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

        public string GetPostText(string link)
        {
            throw new NotImplementedException();
        }
    }
}
