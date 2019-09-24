using BlogPWA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPWA.Services
{
    public interface IBlogService
    {
        Task<BlogViewModel> GetPosts();

        Task<string> GetPostText(string link);

        List<BlogViewModel> GetOlderPosts(int oldestPostId);

        Task<IEnumerable<BlogViewModel>> GetLatestPosts();
    }
}
