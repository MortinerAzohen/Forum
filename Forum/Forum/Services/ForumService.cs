using Forum.Areas.Identity.Data;
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
    public class ForumService : IForumService

    {
        private readonly ForumDbContext _context;
        public ForumService(ForumDbContext context)
        {
            _context = context;
        }
        public Task Create(ForumInstance forum)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int forumId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetAllActiveUsers()
        {
            throw new NotImplementedException();
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

        public Task UpdateForumDescription(int forumId, string newDescritpion)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForumTitle(int forumId, string newTitle)
        {
            throw new NotImplementedException();
        }
    }
}
