using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Models;
using Forum.ViewModels;
using Forum.Interfaces;
using Forum.ViewModels.Post;
using Microsoft.AspNetCore.Mvc;
using Forum.ViewModels.Home;
using Forum.ViewModels.Search;
using Forum.ViewModels.Forum;

namespace Forum.Controllers
{
    public class SearchController : Controller
    {
        private readonly IForumService _forumService;
        private readonly IPostService _postService;
        public SearchController(IForumService forumService, IPostService postService)
        {
            _forumService = forumService;
            _postService = postService;
        }
        public IActionResult Index(int id, string searchQuery)
        {
           var model = SearchPosts(id, searchQuery);
            return View(model);
        }
        private SearchIndexModel SearchPosts(int id, string searchQuery)
        {
            IEnumerable<Post> postsList = Enumerable.Empty<Post>();
            if (id == 0)
            {
               postsList = _postService.GetFiltredPosts(searchQuery);
            }
            else
            {
               postsList = _postService.GetFiltredPosts(searchQuery, id);
            }
            var posts = postsList.Select(post => new PostListingModel
            {
                Id = post.Id,
                Title = post.Title,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                Author = post.User.UserName,
                DatePosted = post.Created.ToString(),
                ForumView = GetForumListingForPost(post),
                RepliesCount = post.Replies.Count()
            });
            return new SearchIndexModel
            {
                LatestPosts = posts,
                SearchQuery = searchQuery,
                ForumId = id
            };
        }

        private ForumListingModel GetForumListingForPost(Post post)
        {
            return new ForumListingModel
            {
                Id = post.Forum.Id,
                Name = post.Forum.Title,
                Description = post.Forum.Description,
                ForumImgUrl = post.Forum.ImageUrl
            };
        }
        [HttpPost]
        public IActionResult Search(string searchQuery)
        {
            int id = 0;
            return RedirectToAction("Index", new { id, searchQuery });
        }
        [HttpPost]
        public IActionResult SearchInForum (ForumTopicModel model)
        {
            int id = model.Forum.Id;
            string searchQuery = model.SearchQuery;
            return RedirectToAction("Index", new { id, searchQuery });
        }
    }
}
