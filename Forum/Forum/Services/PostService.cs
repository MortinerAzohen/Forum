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

        public Task AddReply(PostReply reply)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Edit(int id, string newContent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetFiltredPosts(string search)
        {
            throw new NotImplementedException();
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
    }
}
