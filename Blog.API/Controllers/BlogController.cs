using Blog.API.Model;
using Blog.API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        [HttpGet]
        [Route("posts")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<BlogPost>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<BlogPost>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult LatestBlogPosts([FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0, string ids = null)
        {
            var posts = GetLatestPosts();

            if (!posts.Any())
            {
                return BadRequest("Não foram encontrados posts para exibicao");
            }

            var totalItems = posts.LongCount();

            var itemsOnPage = posts.OrderBy(c => c.Title)
                                    .Skip(pageSize * pageIndex)
                                    .Take(pageSize)
                                    .ToList();

            var model = new PaginatedItemsViewModel<BlogPost>(pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        private List<BlogPost> GetLatestPosts()
        {
            var posts = new List<BlogPost>();

            for (int i = 0; i < 5; i++)
            {
                var postItem = new BlogPost()
                {
                    PostId = i,
                    Title = "xxx",
                    ShortDescription = "xxx"
                };

                posts.Add(postItem);
            }
            return posts;
        }
    }
}