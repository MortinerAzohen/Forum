using Forum.Data;
using Forum.Interfaces;
using Forum.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services
{
    public class PostService : IPostService
    {
        private readonly ForumDbContext _context;
        public PostService(ForumDbContext context)
        {
            _context = context;
        }

        public async Task Add(Post post)
        {
            _context.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var post = _context.posts.Where(p => p.Id == id).First();
            _context.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(int id, string newContent)
        {
            var post  = _context.posts.Where(p => p.Id == id).First();
            post.Content = newContent;
            _context.Attach(post);
            _context.Entry(post).Property("Content").IsModified = true;
            await _context.SaveChangesAsync();

        }

        public IEnumerable<Post> GetAll()
        {
            return _context.posts
                .Include(p => p.User)
                .Include(p => p.Forum)
                .Include(p => p.Replies).ThenInclude(r => r.User);
        }

        public IEnumerable<Post> GetFiltredPosts(string search, int forumId)
        {
            return _context.posts.Where(p=>p.Forum.Id==forumId)
                .Where(p => p.Title.Contains(search) || p.Content.Contains(search) || p.User.Email.Contains(search))
                .Include(p => p.User).Include(p => p.Forum)
                .Include(p => p.Replies).ThenInclude(r => r.User);
        }
        public IEnumerable<Post> GetFiltredPosts(string search)
        {
            return _context.posts.Where(p => p.Title.Contains(search) || p.Content.Contains(search) || p.User.Email.Contains(search))
                .Include(p => p.User).Include(p => p.Forum)
                .Include(p => p.Replies).ThenInclude(r => r.User);
        }

        public IEnumerable<Post> GetLatestPosts(int nPosts)
        {
            return _context.posts.OrderByDescending(p => p.Created).Take(nPosts)
                .Include(p=>p.User).Include(p=>p.Forum)
                .Include(p=>p.Replies).ThenInclude(r=>r.User);
        }

        public Post GetPost(int postId)
        {
            return _context.posts.Where(p => p.Id == postId)
                .Include(p=>p.Forum)
                .Include(p => p.User)
                .Include(p => p.Replies).ThenInclude(r=>r.User)
                .First();
        }

        public IEnumerable<Post> GetPostsByForum(int id)
        {
            return _context.forums.Where(forum => forum.Id == id).First().Posts;
        }
        public async Task AddReply(PostReply reply)
        {
            _context.Add(reply);
            await _context.SaveChangesAsync();
        }
    }
}
