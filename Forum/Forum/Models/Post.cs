using Forum.Areas.Identity.Data;
using System;
using System.Collections.Generic;


namespace Forum.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ForumInstance Forum { get; set; }
        public virtual IEnumerable<PostReply> Replies{get;set;}

    }
}
