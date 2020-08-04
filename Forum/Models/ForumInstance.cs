using System;
using System.Collections.Generic;


namespace Forum.Models
{
    public class ForumInstance
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedMyProperty { get; set; }
        public string ImageUrl { get; set; }
        public virtual IEnumerable<Post> Posts { get; set; }

    }
}
