using Forum.Areas.Identity.Data;
using Forum.Data;
using Forum.Interfaces;
using Forum.Models;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services
{
    public class ForumService : IForumService

    {
        private readonly ForumDbContext _context;
        public ForumService(ForumDbContext context)
        {
            _context = context;
        }
        public async Task Create(ForumInstance forum)
        {
            _context.Add(forum);
            await _context.SaveChangesAsync();
        }
        public async Task Edit(ForumInstance editedForum)
        {
            var forum = _context.forums.Where(f => f.Id == editedForum.Id).First();
            forum.Description = editedForum.Description;
            forum.Title = editedForum.Title;
            forum.ImageUrl = editedForum.ImageUrl;
            _context.Update(forum);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int forumId)
        {
            var forum = _context.forums.Where(f => f.Id == forumId).First();
            _context.forums.Remove(forum);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            var users = _context.Users;
            return users;
        }

        public IEnumerable<ForumInstance> GetAllForums()
        {
            return _context.forums
                .Include(forum => forum.Posts);
        }

        public ForumInstance GetById(int id)
        {

            var forum =  _context.forums.Where(f => f.Id == id)
                                  .Include(f => f.Posts).ThenInclude(f => f.User)
                                  .Include(f => f.Posts).ThenInclude(p => p.Replies).ThenInclude(r => r.User)
                                  .First();
            return forum;
        }

        public async Task UpdateForumDescription(int forumId, string newDescritpion)
        {
            var forum = _context.forums.Where(f => f.Id == forumId).First();
            forum.Description = newDescritpion;
            _context.Attach(forum);
            _context.Entry(forum).Property("Description").IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateForumTitle(int forumId, string newTitle)
        {
            var forum = _context.forums.Where(f => f.Id == forumId).First();
            forum.Title = newTitle;
            _context.Attach(forum);
            _context.Entry(forum).Property("Title").IsModified = true;
            await _context.SaveChangesAsync();
        }
        
    }
}
