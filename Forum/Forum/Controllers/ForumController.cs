
using System;
using System.Linq;
using Forum.Interfaces;
using Forum.Models;
using Forum.ViewModels;
using Forum.ViewModels.Forum;
using Forum.ViewModels.Post;
using Microsoft.AspNetCore.Mvc;


namespace Forum.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForumService _forumService;
        private readonly IPostService _postService;
        public ForumController(IForumService forumService, IPostService postService)
        {
            _forumService = forumService;
            _postService = postService;
        }

        public IActionResult Index()
        {
            var forums = _forumService.GetAllForums().Select(forum => new ForumListingModel { 
                Id=forum.Id,
                Description=forum.Description,
                Name=forum.Title
            });
            var model = new ForumIndexModel
            {
                ForumList = forums
            };
            return View(model);
        }
        public IActionResult Topic(int id)
        {
            var forum = _forumService.GetById(id);
            var posts = forum.Posts;
            var postListings = posts.Select(post => new PostListingModel
            {
                Id = post.Id,
                Author = post.User.UserName,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                Title = post.Title,
                DatePosted = post.Created.ToString(),
                RepliesCount=post.Replies.Count(),
                ForumView = BuildForumListing(post)
            });
            var model = new ForumTopicModel
            {
                posts = postListings,
                Forum = BuildForumListing(forum)
            };
            return View(model);
        }

        private ForumListingModel BuildForumListing(Post post)
        {
            var forum = post.Forum;
            return BuildForumListing(forum);
        }
        private ForumListingModel BuildForumListing(ForumInstance forum)
        {
            return new ForumListingModel
            {
                Id = forum.Id,
                Description = forum.Description,
                Name = forum.Title,
                ForumImgUrl = forum.ImageUrl
            };
        }
    }
}
