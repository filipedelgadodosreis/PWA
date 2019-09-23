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

        [HttpGet]
        [Route("posts/links")]
        [ProducesResponseType(typeof(IEnumerable<BlogPost>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult PostText()
        {
            var posts = GetPostText();

            if (!posts.Any())
            {
                return BadRequest("Não foram encontrados posts para exibicao");
            }

            return Ok(posts);
        }

        private List<BlogPost> GetLatestPosts()
        {
            //var posts = new List<BlogPost>();

            //for (int i = 0; i < 5; i++)
            //{
            //    var postItem = new BlogPost()
            //    {
            //        PostId = i,
            //        Title = "Obter posts via API",
            //        ShortDescription = "Como usar fetch para obter uma lista de posts do blog"
            //    };

            //    posts.Add(postItem);
            //}
            //return posts;

            return new List<BlogPost>() {
                new BlogPost { PostId = 1, Title = "Obter posts via API", ShortDescription = "Como usar fetch para obter uma lista de posts do blog" },
                new BlogPost { PostId = 2, Title = "Usando Indexed DB", ShortDescription = "Como salvar lista de posts utilizando indexed DB" },
                new BlogPost { PostId = 3, Title = "Gravando posts do blog no cache", ShortDescription = "Como usar a Cache API para salvar posts de blog que podem ser acessados offline" },
                new BlogPost { PostId = 4, Title = "Obtendo dado em cache com Service Worker", ShortDescription = "Como utilizar Service Worker para obter dado do cache quando o usuário está offline" },
                new BlogPost { PostId = 5, Title = "Criando uma Web App instalável", ShortDescription = "Como criar arquivos que permitem que você instale o seu aplicativo no seu celular" },
                new BlogPost { PostId = 6, Title = "Enviando notificações push", ShortDescription = "Como enviar notificações push que permitem notificar o usuário que tem o seu aplicativo instalado" },
                new BlogPost { PostId = 7, Title = "Micro front ends", ShortDescription = "Como criar Micro front ends" },
                new BlogPost { PostId = 8, Title = "Blazor", ShortDescription = "Como implementar uma SPA com Blazor client-side" },
                new BlogPost { PostId = 9, Title = "Xamarim", ShortDescription = "Como implementar uma aplicação Xamarim" },
                new BlogPost { PostId = 10, Title = "Unity", ShortDescription = "Como implementar uma aplicação Unity" },
                new BlogPost { PostId = 11, Title = "Angular", ShortDescription = "Como implementar uma aplicação Angular" },
                new BlogPost { PostId = 12, Title = "React", ShortDescription = "Como implementar uma aplicação React" }

            //return new List<BlogPost>() {
            //    new BlogPost { PostId = 1, Title = "Obter posts via API", ShortDescription = "Como usar fetch para obter uma lista de posts do blog" },
            //    new BlogPost { PostId = 2, Title = "Usando Indexed DB", ShortDescription = "Como salvar lista de posts utilizando indexed DB" },
            //    new BlogPost { PostId = 3, Title = "Gravando posts do blog no cache", ShortDescription = "Como usar a Cache API para salvar posts de blog que podem ser acessados offline" },
        };

        }

        private List<BlogPost> GetPostText()
        {
            return new List<BlogPost>() {
                new BlogPost { PostId = 1, Title = "Obter posts via API", ShortDescription = "Como usar fetch para obter uma lista de posts do blog" },
                new BlogPost { PostId = 2, Title = "Usando Indexed DB", ShortDescription = "Como salvar lista de posts utilizando indexed DB" },
                new BlogPost { PostId = 3, Title = "Gravando posts do blog no cache", ShortDescription = "Como usar a Cache API para salvar posts de blog que podem ser acessados offline" },
                new BlogPost { PostId = 4, Title = "Obtendo dado em cache com Service Worker", ShortDescription = "Como utilizar Service Worker para obter dado do cache quando o usuário está offline" },
                new BlogPost { PostId = 5, Title = "Criando uma Web App instalável", ShortDescription = "Como criar arquivos que permitem que você instale o seu aplicativo no seu celular" },
                new BlogPost { PostId = 6, Title = "Enviando notificações push", ShortDescription = "Como enviar notificações push que permitem notificar o usuário que tem o seu aplicativo instalado" },
                new BlogPost { PostId = 7, Title = "Micro front ends", ShortDescription = "Como criar Micro front ends" },
                new BlogPost { PostId = 8, Title = "Blazor", ShortDescription = "Como implementar uma SPA com Blazor client-side" },
                new BlogPost { PostId = 9, Title = "Xamarim", ShortDescription = "Como implementar uma aplicação Xamarim" },
                new BlogPost { PostId = 10, Title = "Unity", ShortDescription = "Como implementar uma aplicação Unity" },
                new BlogPost { PostId = 11, Title = "Angular", ShortDescription = "Como implementar uma aplicação Angular" },
                new BlogPost { PostId = 12, Title = "React", ShortDescription = "Como implementar uma aplicação React" }
            };
        }
    }
}