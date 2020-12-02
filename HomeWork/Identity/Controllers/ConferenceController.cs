using Identity.Context;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Identity.Controllers
{
    [Authorize (Roles = "Admin, Chair, Verified User")]
    public class ConferenceController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public ConferenceController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var activeuser = await _userManager.FindByNameAsync(User.Identity.Name);
            var role = await _userManager.GetRolesAsync(activeuser);
            using var context = new IdentityContext();
            if (role.Contains("Admin"))
            {
                return View("AdminView", context.Conferences.ToList());
            }

            return View(context.Conferences.Where(I => I.CreateUserId == activeuser.Id).ToList());


        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddConferenceViewModel addConferenceViewModel)
        {
            if (ModelState.IsValid)
            {
                using var context = new IdentityContext();
                var activeUser = await _userManager.FindByNameAsync(User.Identity.Name);

                await context.Conferences.AddAsync(new Conference
                {
                    CreateionDateTime = addConferenceViewModel.CreateionDateTime,
                    Name = addConferenceViewModel.Name,
                    EndDate = addConferenceViewModel.EndDate,
                    ShortName = addConferenceViewModel.ShortName,
                    StartDate = addConferenceViewModel.StartDate,
                    SubmissionDeadline = addConferenceViewModel.SubmissionDeadline,
                    WebSite = addConferenceViewModel.WebSite,
                    Year = addConferenceViewModel.Year,
                    CreateUserId = activeUser.Id
                });
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(addConferenceViewModel);
        }


        public IActionResult Delete(int id)
        {
            using var context = new IdentityContext();
            var conference = context.Conferences.Find(id);
            context.Conferences.Remove(conference);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            using var context = new IdentityContext();
            var conference = context.Conferences.Find(id);

            UpdateConferenceViewModel model = new UpdateConferenceViewModel
            {
                ConfID = conference.ConfID,
                CreateionDateTime = conference.CreateionDateTime,
                CreateUserId = conference.CreateUserId,
                EndDate = conference.EndDate,
                Name = conference.Name,
                ShortName = conference.ShortName,
                StartDate = conference.StartDate,
                SubmissionDeadline = conference.SubmissionDeadline,
                WebSite = conference.WebSite,
                Year = conference.Year
            };


            return View("Edit", model);
        }

        [HttpPost]
        public IActionResult Edit(UpdateConferenceViewModel model)
        {
            if (ModelState.IsValid)
            {
                using var context = new IdentityContext();
                var uconference = context.Conferences.Find(model.ConfID);

                uconference.CreateionDateTime = model.CreateionDateTime;
                uconference.CreateionDateTime = model.CreateionDateTime;
                uconference.CreateUserId = model.CreateUserId;
                uconference.EndDate = model.EndDate;
                uconference.Name = model.Name;
                uconference.ShortName = model.ShortName;
                uconference.StartDate = model.StartDate;
                uconference.SubmissionDeadline = model.SubmissionDeadline;
                uconference.WebSite = model.WebSite;
                uconference.Year = model.Year;

                context.Update(uconference);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }


        public IActionResult AdminView()
        {
            return View();
        }
    }
}
