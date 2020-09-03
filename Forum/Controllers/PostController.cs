using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Forum.Models;
using Forum.Interfaces;
using Forum.ViewModels.Posts;
using Forum.ViewModels.Reply;
using Forum.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;

namespace Forum.Controllers
{
    public class PostController: Controller
    {
        private readonly IPostService _postService;
        private readonly IForumService _forumService;
        private static UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationService _authorizationService;


        public PostController(IAuthorizationService authorizationService,IPostService postService, IForumService forumService, UserManager<ApplicationUser>userManager)
        {
            _postService = postService;
            _forumService = forumService;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }
        public IActionResult Index(int id)
        {
            var post = _postService.GetPost(id);
            var model = new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                DatePosted = post.Created,
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                AuthorRating = post.User.Rating,
                PostContent = post.Content,
                Repies = BuildPostReplies(post.Replies),
                forum=post.Forum
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditPostViewModel model)
        {
            var post = _postService.GetPost(model.PostId);
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, post.User.Id, "UserPolicy");
            if(authorizationResult.Succeeded || User.IsInRole("Admin"))
            {
                await _postService.EditContent(model.PostId, model.Content);
                await _postService.EditTitle(model.PostId, model.Title);
            }
            return RedirectToAction("Index", "Post", new { id = model.PostId });
        }
        public async Task<IActionResult> Delete(int id)
        {
            var post = _postService.GetPost(id);
            await _postService.Delete(post.Id);
            return RedirectToAction("Topic", "Forum", new { id = post.Forum.Id });
        }
        public async Task<IActionResult> Edit(int id)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            var post = _postService.GetPost(id);
            var model = new EditPostViewModel
            {
                Content = post.Content,
                Title =post.Title,
                AuthorId = user.Id,
                AuthorName=user.UserName,
                PostId = post.Id
            };
            return View(model);
        }
        public IActionResult Create(int id)
        {
            if(!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index","Home"); //temporary, must create register view other than basic
            }
            var forum = _forumService.GetById(id);
            var model = new NewPostModelView
            {
                ForumName = forum.Title,
                ForumId  = forum.Id,
                ForumImgUrl = forum.ImageUrl,
                AuthorName = User.Identity.Name
            };
            return View(model);
        }
        public IActionResult CreateReply(int id)
        {
            var post = _postService.GetPost(id);
            var model = new NewReplyModelView
            {
                PostTitle = post.Title,
                PostId=post.Id,
                AuthorName=User.Identity.Name,
                PostContent=post.Content
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddPost(NewPostModelView newPostModelView)
        {
            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;
            var post = BuildPost(newPostModelView, user);
            _postService.Add(post).Wait();
            return RedirectToAction("Index", "Post",new { id = post.Id });
        }
        public async Task<IActionResult> AddReply(NewReplyModelView newReplyModelView)
        {
            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;
            var reply = BuildReply(newReplyModelView, user);
            _postService.AddReply(reply).Wait();
            return RedirectToAction("Index", "Post", new { id = reply.Post.Id });     
        }

        private PostReply BuildReply(NewReplyModelView newReplyModelView, ApplicationUser user)
        {
            var post = _postService.GetPost(newReplyModelView.PostId);
            return new PostReply
            {
                Title = newReplyModelView.Title,
                Content = newReplyModelView.ReplyContent,
                Created = DateTime.Now,
                User = user,
                Post = post
            };
        }

        private Post BuildPost(NewPostModelView newPostModelView, ApplicationUser user)
        {
            var forum = _forumService.GetById(newPostModelView.ForumId);
            return new Post
            {
                Title = newPostModelView.Title,
                Content = newPostModelView.Content,
                Created = DateTime.Now,
                User = user,
                Forum = forum
            };
        }

        private IEnumerable<PostReplyModel> BuildPostReplies(IEnumerable<PostReply> replies)
        {
            return replies.Select(reply => new PostReplyModel
            {
                Id=reply.Id,
                Title= reply.Title,
                DatePosted = reply.Created,
                AuthorId = reply.User.Id,
                AuthorName = reply.User.UserName,
                AuthorRating = reply.User.Rating,
                ReplyContent = reply.Content,
                PostId = reply.Post.Id
            }
            );
        }
    }
}
