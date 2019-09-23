using System.Collections.Generic;

namespace BlogPWA.Models
{
    public class BlogViewModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public List<BlogPost> Data { get; set; }
    }
}
