using Forum.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.Administration
{
    public class CreateRoleViewModel
    {
        public string RoleName { get; set; }
        public IList<RoleIsSelectedModel> Users {get;set;}
    }
}
