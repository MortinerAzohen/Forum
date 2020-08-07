using Forum.Areas.Identity.Data;
using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Interfaces
{
    public interface IForumService
    {
        ForumInstance GetById(int id);
        IEnumerable<ForumInstance> GetAllForums();
        IEnumerable<ApplicationUser> GetAllUsers();
        Task Edit(ForumInstance forum);
        Task Create(ForumInstance forum);
        Task Delete(int forumId);
        Task UpdateForumTitle(int forumId, string newTitle);
        Task UpdateForumDescription(int forumId, string newDescritpion);
    }
}
