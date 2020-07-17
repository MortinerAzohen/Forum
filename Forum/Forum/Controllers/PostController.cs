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

namespace Forum.Controllers
{
    public class PostController: Controller
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
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
                Repies = BuildPostReplies(post.Replies)
            };
            return View(model);
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
