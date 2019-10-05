define(['./template.js', '../lib/showdown/showdown.js'],
    function (template, showdown) {

        var oldestBlogPostId = 0;
        var blogPostUrl = '/Home/Post/?link=';
        var blogLatestPostsUrl = '/Home/LatestBlogPosts/';
        var blogMorePostsUrl = '/Home/MoreBlogPosts/?oldestBlogPostId=';


        function fetchPromise(url) {
            return new Promise(function (resolve, reject) {
                fetch(url)
                    .then(function (response) {
                        return response.json();
                    }).then(function (data) {
                        template.appendBlogList(data);
                        setOldestBlogPostId(data);
                        resolve('The connection is OK, showing latest results');
                    }).catch(function (e) {
                        resolve('No connection, showing offline results');
                    });
                setTimeout(function () {
                    resolve('The connection is hanging, showing offline results');
                }, 5000);
            });
        }

        function loadData(url) {
            fetchPromise(url)
                .then(function (status) {
                    $('#connection-status').html(status);
                });
        }


        function setOldestBlogPostId(data) {
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
