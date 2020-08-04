using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Areas.Identity.Data;
using Forum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Forum.Authorization
{
    public class UserAuthorizationHandler:AuthorizationHandler<SameAuthorRequirement,string>
    {
        UserManager<ApplicationUser> _userManager;
        public UserAuthorizationHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameAuthorRequirement requirement, string resource)
        {
            if(context.User==null || resource==null)
            {
                return Task.CompletedTask;
            }
            if(resource == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
                
            }
            return Task.CompletedTask;
        }
    }
    public class SameAuthorRequirement : IAuthorizationRequirement { }
}

