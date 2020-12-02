using System;
using System.Collections.Generic;
using System.IO;
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
    public class PanelController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        public PanelController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(user);
        }

        [AllowAnonymous]
        public IActionResult HerkesErissin()
        {

            return View();
        }

        public async Task<IActionResult> UpdateUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            UserUpdateViewModel userUpdateViewModel = new UserUpdateViewModel
            {
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                PictureUrl = user.PictureUrl

            };
            return View(userUpdateViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(UserUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (model.Picture != null)
                {
                    var uygulamanincalistigiyer = Directory.GetCurrentDirectory();
                    var uzanti = Path.GetExtension(model.Picture.FileName);
                    var resimAd = Guid.NewGuid() + uzanti;
                    var kaydedilecekYer = uygulamanincalistigiyer + "/wwwroot/img/" + resimAd;


                    using var stream = new FileStream(kaydedilecekYer, FileMode.Create);
                    await model.Picture.CopyToAsync(stream);
                    user.PictureUrl = resimAd;
                }

                user.Name = model.Name;
                user.SurName = model.Surname;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;


                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Panel");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }


        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
