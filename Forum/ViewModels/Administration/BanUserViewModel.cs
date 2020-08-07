using Forum.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.Administration
{
    public class BanUserViewModel
    {
        public string Email { get; set; }
        public string UserId { get; set; }
        public int BannedForXdays { get; set; }
    }
}
