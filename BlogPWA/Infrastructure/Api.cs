namespace BlogPWA.Infrastructure
{
    public static class Api
    {
        public static class Blog
        {
            public static string GetLatestPosts(string baseUri)
            {
                return $"{baseUri}posts";
            }

            public static string GetPostsLinks(string baseUri)
            {
                return $"{baseUri}posts/links";
            }
        }
    }
}
