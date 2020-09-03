
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Areas.Identity.Data;
using Forum.Interfaces;
using Forum.ViewModels.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Forum.Controllers
{
    public class AdministrationController:Controller
    {
        private static UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IForumService _forumService;
        private readonly IPostService _postService;

        public AdministrationController(IPostService postService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleMenager, IForumService forumService )
        {
            _userManager = userManager;
            _roleManager= roleMenager;
            _forumService = forumService;
            _postService = postService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            var model = new UserViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                UserImgUrl = user.ProfileImgUrl,
                UserRating = user.Rating,
                NickName = user.NickName,
                UserRoles = roles
            };
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BanUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var model = new BanUserViewModel
            {
                UserId = user.Id,
                Email = user.Email
            };
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BanUser(BanUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            await _userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddDays(model.BannedForXdays));
    
            return RedirectToAction("ShowUsers","administration");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ShowSpecificUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var model = new UserListModel
            {
                UserId = user.Id,
                Email = user.Email,
                UserImgUrl = user.ProfileImgUrl,
                UserRating = user.Rating,
                UserRoles = await _userManager.GetRolesAsync(user),
                LockoutEnd = user.LockoutEnd,
                UserPostList = _postService.GetPostsByAuthor(id).Select(post => new ViewModels.Posts.PostViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    DatePosted = post.Created,
                    AuthorId = post.User.Id,
                    AuthorName = post.User.UserName,
                    AuthorRating = post.User.Rating,
                    PostContent = post.Content,
                    forum = post.Forum
                })
            };
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult ShowUsers()
        {
            var users = _forumService.GetAllUsers();
            var model = BuildUserList(users);
            return View(model);
        }
        private IEnumerable<UserListModel> BuildUserList(IEnumerable<ApplicationUser> users)
        {
            return users.Select(user => new UserListModel
            {
                UserId = user.Id,
                UserRating = user.Rating,
                Email = user.Email,
                LockoutEnd = user.LockoutEnd,
                IsBanned = CheckIfBanned(user),
                UserPostList = _postService.GetPostsByAuthor(user.Id).Select(post => new ViewModels.Posts.PostViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    DatePosted = post.Created,
                    AuthorId = post.User.Id,
                    AuthorName = post.User.UserName,
                    AuthorRating = post.User.Rating,
                    PostContent = post.Content,
                    forum = post.Forum
                })
            }) ;
        }

        private bool CheckIfBanned(ApplicationUser user)
        {
            if(user.LockoutEnd>DateTimeOffset.Now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateRole()
        {
            var users = _userManager.Users;
            var model = new CreateRoleViewModel
            {
                Users = BuildUserIsSelectedView(users)
            };
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUserToRole()
        {
            var model = new RolesMenagerViewModel
            {
                listOfUsersWithRoles = await BuildUserWithRoles()
            };
            return View(model);
        }

        private async Task<List<AddUserToRolesViewModel>> BuildUserWithRoles()
        {
            var roles = _roleManager.Roles;
            var users = _userManager.Users;
            var model = new List<AddUserToRolesViewModel>();
            foreach (var user in users)
            {
                var userRoles = new List<UserIsInRoles>();
                foreach (var role in roles)
                {
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        userRoles.Add(new UserIsInRoles
                        {
                            RoleName = role.Name,
                            IsInRole = true
                        });
                    }
                    else
                    {
                        userRoles.Add(new UserIsInRoles
                        {
                            RoleName = role.Name,
                            IsInRole = false
                        });
                    }
                }
                model.Add(new AddUserToRolesViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    UserEmail = user.Email,
                    UserRoles = userRoles
                });
            }
            return model;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddUserToRole(RolesMenagerViewModel model)
        {
            foreach(var userWithRoles in model.listOfUsersWithRoles)
            {
                var appUser = await _userManager.FindByIdAsync(userWithRoles.UserId);
                foreach(var role in userWithRoles.UserRoles)
                {
                    if((await _userManager.IsInRoleAsync(appUser,role.RoleName)==true) && (role.IsInRole==false))
                    {
                        await _userManager.RemoveFromRoleAsync(appUser, role.RoleName);
                    }
                    else if ((await _userManager.IsInRoleAsync(appUser, role.RoleName)==false) && (role.IsInRole == true))
                    {
                        await _userManager.AddToRoleAsync(appUser, role.RoleName);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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
                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                    await _userManager.AddToRoleAsync(appUser, model.RoleName);
                }
            }
            return RedirectToAction("Index","Home");
        }


        private IList<RoleIsSelectedModel> BuildUserIsSelectedView(IQueryable<ApplicationUser> users)
        {
            return users.Select(user => new RoleIsSelectedModel
            { 
                UserEmail = user.Email,
                UserId = user.Id,
                UserName = user.UserName,
                IsInNewRole = false
            }).ToList();
        }
        
    }
}
