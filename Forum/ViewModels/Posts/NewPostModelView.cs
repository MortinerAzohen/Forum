using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.Posts
{
    public class NewPostModelView
    {
        public int ForumId { get; set; }
        public string ForumName { get; set; }
        public string AuthorName { get; set; }
        public string ForumImgUrl { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
    }
}
