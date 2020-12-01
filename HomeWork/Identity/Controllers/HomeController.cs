using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Context;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(new UserSignInViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserSignInViewModel userSignInViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(userSignInViewModel.UserName, userSignInViewModel.Password, userSignInViewModel.RememberMe, true);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Panel");
                }

                if (result.IsLockedOut)
                {
                    var gelen = await _userManager.GetLockoutEndDateAsync(await _userManager.FindByNameAsync(userSignInViewModel.UserName));

                    var kısıtlananSure = gelen.Value;
                    var kalanDakika = kısıtlananSure.Minute - DateTime.Now.Minute;

                    ModelState.AddModelError("", $"5 kere yanlış giriş yapıldığı için hesabınız kitlenmiştir. {kalanDakika} kadar hesabınız bloklu olarak kalacaktır.");
                    return View("Index", userSignInViewModel);
                }

                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Email doğrulaması gereklidir.");
                    return View("Index", userSignInViewModel);
                }

                var yanlisGirisSayisi = await _userManager.GetAccessFailedCountAsync(await _userManager.FindByNameAsync(userSignInViewModel.UserName));
                ModelState.AddModelError("", $"Kullanıcı adı veya parola hatalı. {5 - yanlisGirisSayisi}  kadar yanlış girerseniz hesabını bloklanacaktır.");
            }
            return View("Index", userSignInViewModel);
        }

        public IActionResult KayıtOl() => View(new UserSignUpViewModel());

        [HttpPost]
        public async Task<IActionResult> KayıtOl(UserSignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    Email = model.Email,
                    Name = model.Name,
                    SurName = model.SurName,
                    UserName = model.UserName

                };
                var result = await _userManager.CreateAsync(appUser, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        public IActionResult AccessDenied() => View();
    }
}
