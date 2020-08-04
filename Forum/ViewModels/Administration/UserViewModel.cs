using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.Administration
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string UserImgUrl { get; set; }
        public IList<string> UserRoles { get; set; }
        public int UserRating { get; set; }
        public string NickName { get; set; }

    }
}
