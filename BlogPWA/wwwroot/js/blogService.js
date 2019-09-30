define(['./template.js', '../lib/showdown/showdown.js'],
    function (template, showdown) {

        var oldestBlogPostId = 0;
        var blogPostUrl = '/Home/Post/?link=';
        var blogLatestPostsUrl = '/Home/LatestBlogPosts/';
        var blogMorePostsUrl = '/Home/MoreBlogPosts/?oldestBlogPostId=';        function loadData(url) {
            fetch(url)
                .then(function (response) {
                    return response.json();
                }).then(function (data) {
                    template.appendBlogList(data);
                    setOldestBlogPostId(data);
                });
        }        function setOldestBlogPostId(data) {
            var ids = data.result.data.map(item => item.postId);
            oldestBlogPostId = Math.min(...ids);
        }
        function loadBlogPost(link) {
            fetch(blogPostUrl + link)
                .then(function (response) {
                    return response.text();
                }).then(function (data) {
                    var converter = new showdown.Converter();
                    html = converter.makeHtml(data);
                    template.showBlogItem(html, link);
                    window.location = '#' + link;
                });
        }


        function loadLatestBlogPosts() {
            loadData(blogLatestPostsUrl);
        }
        function loadMoreBlogPosts() {
            loadData(blogMorePostsUrl + oldestBlogPostId);
        }
        return {
            loadLatestBlogPosts: loadLatestBlogPosts,
            loadBlogPost: loadBlogPost,
            loadMoreBlogPosts: loadMoreBlogPosts
        }
    });