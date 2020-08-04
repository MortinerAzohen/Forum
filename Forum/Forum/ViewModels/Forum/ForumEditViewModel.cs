using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.Forum
{
    public class ForumEditViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ForumUrl { get; set; }
        public string Title { get; set; }
        public IFormFile ForumImg { get; set; }
    }
}
