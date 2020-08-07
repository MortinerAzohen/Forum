using Forum.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.Interfaces
{
    public interface IPostService
    {
        Post GetPost(int postId);
        IEnumerable<Post> GetFiltredPosts(string search);
        IEnumerable<Post> GetFiltredPosts(string search, int forumId);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetPostsByForum(int id);
        Task Add(Post post);
        Task EditContent(int id, string newContent);
        Task EditTitle(int id, string newTitle);
        Task Delete(int id);
        Task AddReply(PostReply reply);
        IEnumerable<Post> GetLatestPosts(int nPosts);
        IEnumerable<Post> GetPostsByAuthor(string authorId);
    }
}
