using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Context;
using Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }

        public IActionResult AddRole()
        {
            return View(new AddRoleViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppRole appRole = new AppRole
                {
                    Name = model.RoleName

                };

                var result = await _roleManager.CreateAsync(appRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View();
        }

        public IActionResult UpdateRole(int id)
        {
            AppRole role = _roleManager.Roles.Where(x => x.Id == id).FirstOrDefault();
            UpdateRoleViewModel updateRoleViewModel = new UpdateRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            return View(updateRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateRoleViewModel model)
        {
            var tobeUpdatedRole = _roleManager.Roles.Where(I => I.Id == model.Id).FirstOrDefault();
            tobeUpdatedRole.Name = model.RoleName;
            var result = await _roleManager.UpdateAsync(tobeUpdatedRole);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);

        }

        public async Task<IActionResult> DeleteRole(int id)
        {
            var tobedeletedRole = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            var result = await _roleManager.DeleteAsync(tobedeletedRole);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            TempData["errors"] = result.Errors;
            return RedirectToAction("Index");
        }

        public IActionResult UserList()
        {
            return View(_userManager.Users.ToList());
        }


        public async Task<IActionResult> AssignRole(int id)
        {
            var user = _userManager.Users.FirstOrDefault(X => X.Id == id);

            TempData["userid"] = user.Id;
            var roles = _roleManager.Roles.ToList();
            var userRoles = await _userManager.GetRolesAsync(user);

            List<RoleAssignViewmodel> roleAssignViewmodels = new List<RoleAssignViewmodel>();

            foreach (var item in roles)
            {
                RoleAssignViewmodel model = new RoleAssignViewmodel();
                model.RoleId = item.Id;
                model.RoleName = item.Name;
                model.Exist = userRoles.Contains(item.Name);
                roleAssignViewmodels.Add(model);
            }

            return View(roleAssignViewmodels);

        }



        [HttpPost]
        public async Task<IActionResult> AssignRole(List<RoleAssignViewmodel> models)
        {
            var userId = (int)TempData["userid"];
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);

            foreach (var item in models)
            {
                if (item.Exist)
                {
                    await _userManager.AddToRoleAsync(user, item.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
            }

            return RedirectToAction("UserList");
        }
    }
}
