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

        // GET api/v1/[controller]/posts[?pageSize=3&pageIndex=10]
        /// <summary>
        /// Método responsável por retornar as postagens mais recentes do blog.
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="ids"></param>
        /// <returns>Objeto com as ultimas postagens</returns>
        [HttpGet]
        [Route("posts")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<BlogPost>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<BlogPost>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult LatestBlogPosts([FromQuery]int pageSize = 3, [FromQuery]int pageIndex = 0)
        {
            var posts = GetPosts();

            if (!posts.Any())
            {
                return BadRequest("Não foram encontrados posts para exibicao");
            }

            var totalItems = posts.LongCount();

            var itemsOnPage = posts.OrderByDescending(c => c.PostId)
                                    .Skip(pageSize * pageIndex)
                                    .Take(pageSize)
                                    .ToList();

            var model = new PaginatedItemsViewModel<BlogPost>(pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        // GET api/v1/[controller]/posts/links[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("posts/links/{id:int}")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<BlogPost>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<BlogPost>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult PostText([FromQuery]int pageSize = 3, [FromQuery]int pageIndex = 0, int id = 0)
        {
            var posts = GetPosts();

            if (!posts.Any())
            {
                return BadRequest("Não foram encontrados posts para exibicao");
            }

            var totalItems = posts.LongCount();

            var itemsOnPage = posts.Where(x => x.PostId < id)
                                    .OrderByDescending(c => c.PostId)
                                    .Skip(pageSize * pageIndex)
                                    .Take(pageSize)
                                    .ToList();

            var model = new PaginatedItemsViewModel<BlogPost>(pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        /// <summary>
        /// Metodo responsavel por recuperar os posts do blog
        /// </summary>
        /// <returns>Retorna lista de posts</returns>
        private List<BlogPost> GetPosts()
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