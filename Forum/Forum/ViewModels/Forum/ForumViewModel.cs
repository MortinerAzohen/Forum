using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.Forum
{
    public class ForumViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile ForumImg{ get; set; }

    }
}
