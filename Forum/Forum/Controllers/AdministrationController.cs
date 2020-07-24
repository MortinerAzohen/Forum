
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Areas.Identity.Data;
using Forum.Interfaces;
using Forum.Models;
using Forum.ViewModels;
using Forum.ViewModels.Administration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Forum.Controllers
{
    public class AdministrationController:Controller
    {
        private static UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdministrationController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleMenager)
        {
            _userManager = userManager;
            _roleManager= roleMenager;
        }
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            //var roles = await _userManager.GetRolesAsync(user);
            var model = new UserViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                UserImgUrl = user.ProfileImgUrl,
                UserRating = user.Rating,
                NickName = user.NickName,
                //UserRoles = roles
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            var users = _userManager.Users;
            var model = new CreateRoleViewModel
            {
                Users = BuildUserIsSelectedView(users)
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name=model.RoleName
            };
            await _roleManager.CreateAsync(identityRole);
            foreach(var user in model.Users)
            {
                if(user.IsInNewRole==true)
                {
                    //add role to user
                }
                else
                {
                    //dont add
                }
            }
            return RedirectToAction("Index","Home");
        }

        private IEnumerable<RoleIsSelectedModel> BuildUserIsSelectedView(IQueryable<ApplicationUser> users)
        {
            return users.Select(user => new RoleIsSelectedModel
            { 
                UserEmail = user.Email,
                UserId = user.Id,
                UserName = user.UserName,
                IsInNewRole = false
            });
        }
    }
}
