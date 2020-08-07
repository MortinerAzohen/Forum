using Forum.ViewModels.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.Administration
{
    public class UserListModel : UserViewModel
    {
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool IsBanned { get; set; }
        public IEnumerable<PostViewModel> UserPostList { get; set; }
    }
}
