
using System;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Forum.Interfaces;
using Forum.Models;
using Forum.ViewModels;
using Forum.ViewModels.Forum;
using Forum.ViewModels.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;

namespace Forum.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForumService _forumService;
        private readonly IPostService _postService;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ForumController(IForumService forumService, IPostService postService, IWebHostEnvironment hostEnvironment)
        {
            _forumService = forumService;
            _postService = postService;
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            var forums = _forumService.GetAllForums().Select(forum => new ForumListingModel
            {
                Id = forum.Id,
                Description = forum.Description,
                Name = forum.Title,
                ForumImgUrl = CreateCorectUrlString(forum.ImageUrl)

            }); 
            var model = new ForumIndexModel
            {
                ForumList = forums
            };
            return View(model);
        }

        private string CreateCorectUrlString(string imageUrl)
        {
            string corectUrlString;
            if (string.IsNullOrEmpty(imageUrl))
            {
                corectUrlString = "/images/forum/fb.jpg";
            }
            else if(!imageUrl.Contains("/images/"))
            {
                corectUrlString = string.Concat("/images/forum/", imageUrl);
            }
            else
            {
                return imageUrl;
            }
            return corectUrlString;
        }

        [Authorize(Roles ="Admin")]
        public IActionResult CreateForum()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateForum(ForumViewModel model)
        {
            string uniqueFileName = UploadedFile(model);
            var forum = new ForumInstance
            {
                Title = model.Title,
                Description = model.Description,
                ImageUrl = uniqueFileName,
                CreatedMyProperty = DateTime.Now
            };
            await _forumService.Create(forum);
            return RedirectToAction("Index", "Forum");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteForum(int forumId)
        {
            await _forumService.Delete(forumId);
            return RedirectToAction("Index", "Forum");
        }
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Mode")]
        public IActionResult EditForum(int forumId)
        {
            var forum = _forumService.GetById(forumId);
            var model = new ForumEditViewModel
            {
                Id = forum.Id,
                Description = forum.Description,
                Title = forum.Title,
                ForumUrl = forum.ImageUrl
            };
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Mode")]
        public async Task<IActionResult> EditForum(ForumEditViewModel model)
        {
            var correctUrl = model.ForumUrl;
            if (!(model.ForumImg ==null))
            {
                correctUrl = UploadedFile(model);
            }
            var forum = new ForumInstance
            {
                Id=model.Id,
                Title = model.Title,
                Description = model.Description,
                CreatedMyProperty = DateTime.Now,
                ImageUrl = correctUrl
            };
            await _forumService.Edit(forum);
            return RedirectToAction("Topic", "Forum",new {id = model.Id});
        }
        private string UploadedFile(ForumViewModel model)
        {
            string uniqueFileName = null;
            if(model.ForumImg!=null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/forum" );
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ForumImg.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ForumImg.CopyTo(fileStream);
                }  
            }
            return uniqueFileName;
        }
        private string UploadedFile(ForumEditViewModel model)
        {
            string uniqueFileName = null;
            if (model.ForumImg != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/forum");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ForumImg.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ForumImg.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
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
