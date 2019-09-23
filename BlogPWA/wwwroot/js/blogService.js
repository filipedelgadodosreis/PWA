//define(['./template.js'], function (template) {
//    var blogPostUrl = '/Home/LatestBlogPosts/';
//    function loadLatestBlogPosts() {
//        fetch(blogPostUrl)
//            .then(function (response) {
//                return response.json();
//            }).then(function (data) {
//                template.appendBlogList(data);
//            });
//    }
//    return {
//        loadLatestBlogPosts: loadLatestBlogPosts
//    }
//});define(['./template.js', '../lib/showdown/showdown.js'],
    function (template, showdown) {

        var blogPostUrl = '/Home/Post/?link=';
        var blogLatestPostsUrl = '/Home/LatestBlogPosts/';

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
            fetch(blogLatestPostsUrl)
                .then(function (response) {
                    return response.json();
                }).then(function (data) {
                    template.appendBlogList(data);
                });
        }

        return {
            loadLatestBlogPosts: loadLatestBlogPosts,
            loadBlogPost: loadBlogPost
        }
    });