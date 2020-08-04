using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.Reply
{
    public class NewReplyModelView
    {
        public string PostTitle { get; set; }
        public int PostId { get; set; }
        public string AuthorName { get; set; }
        public string PostContent { get; set; }
        public string ReplyContent { get; set; }
        public string Title { get; set; }
    }
}
