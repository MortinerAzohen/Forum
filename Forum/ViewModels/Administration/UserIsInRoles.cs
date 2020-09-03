using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.Administration
{
    public class UserIsInRoles
    {
        public string RoleName { get; set; }
        public bool IsInRole{ get; set; }
    }
}
