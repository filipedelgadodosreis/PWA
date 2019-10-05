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

            public static string GetOlderPosts(string baseUri, int oldestPostId)
            {
                return $"{baseUri}posts/links/{oldestPostId}?pageSize=3&pageIndex=0";
            }

            public static string GetPostText(string baseUri)
            {
                return $"{baseUri}posts/links";
            }
        }
    }
}
