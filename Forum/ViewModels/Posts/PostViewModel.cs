using Forum.Models;
using Forum.ViewModels.Reply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.Posts
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DatePosted { get; set; }
        public string AuthorId  { get; set; }
        public string AuthorName { get; set; }
        public int AuthorRating { get; set; }
        public string PostContent  { get; set; }
        public IEnumerable<PostReplyModel> Repies { get; set; }
        public ForumInstance forum { get; set; }


    }
}
