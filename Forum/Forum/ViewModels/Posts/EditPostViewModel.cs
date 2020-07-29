using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.Posts
{
    public class EditPostViewModel
    {
        public string Content { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public int PostId { get; set; }
        public string AuthorId { get; set; }
    }
}
