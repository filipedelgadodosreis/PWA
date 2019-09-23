using BlogPWA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPWA.Services
{
    public interface IBlogService
    {
        Task<BlogViewModel> GetLatestPosts();

        string GetPostText(string link);

        List<BlogViewModel> GetOlderPosts(int oldestPostId);
    }
}
