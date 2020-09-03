
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Forum.Models;
using System;
using Forum.ViewModels.Home;
using Microsoft.Extensions.Options;
using Forum.Interfaces;
using System.Linq;
using Forum.ViewModels.Post;
using Forum.ViewModels;

namespace Forum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService _postService;

        public HomeController(ILogger<HomeController> logger ,IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }

        public IActionResult Index()
        {
            var model = BuildHomeIndexModel();

            return View(model);
        }

        private HomeIndexModel BuildHomeIndexModel()
        {
            var latestPosts = _postService.GetLatestPosts(10);
            var posts = latestPosts.Select(post => new PostListingModel {
                Id= post.Id,
                Title = post.Title,
                AuthorId = post.User.Id,
                AuthorRating= post.User.Rating,
                Author = post.User.UserName,
                DatePosted = post.Created.ToString(),
                ForumView = GetForumListingForPost(post),
                RepliesCount = post.Replies.Count()

            });
            return new HomeIndexModel
            {
                LatestPosts = posts,
                SearchQuery = ""
            };
        }

        private ForumListingModel GetForumListingForPost(Post post)
        {
            return new ForumListingModel
            {
                Id = post.Forum.Id,
                Name = post.Forum.Title,
                Description = post.Forum.Description,
                ForumImgUrl = post.Forum.ImageUrl,
                PostsCount = post.Forum.Posts.Count()
            };
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
