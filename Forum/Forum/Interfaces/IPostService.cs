using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Interfaces
{
    public interface IPostService
    {
        Post GetPost(int postId);
        IEnumerable<Post> GetFiltredPosts(string search);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetPostsByForum(int id);
        Task Add(Post post);
        Task Edit(int id, string newContent);
        Task Delete(int id);
        Task AddReply(PostReply reply);
    }
}
