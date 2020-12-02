using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Context;
using Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddTag(int id)
        {
            using var context = new IdentityContext();
            AddTagViewModel model = new AddTagViewModel();
            model.Conference = await context.Conferences.FindAsync(id);
            return View(model);
        }

        public async Task<IActionResult> AddSubmission(int id)
        {
            using var context = new IdentityContext();
            AddSubmissionViewModel model = new AddSubmissionViewModel();
            model.Conference = await context.Conferences.FindAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddSubmission(AddSubmissionViewModel model)
        {
            if (ModelState.IsValid)
            {
                using var context = new IdentityContext();
                await context.Submissions.AddAsync(new Submissions
                {
                    Submission=model.Submission,
                    ConfId = model.Conference.ConfID,
                    prevSubmissionID = model.prevSubmission
                });

                await context.SaveChangesAsync();
                return RedirectToAction("Index", "Conference");

            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(AddTagViewModel model)
        {
            if (ModelState.IsValid)
            {
                using var context = new IdentityContext();
                await context.ConferenceTags.AddAsync(new ConferenceTags
                {
                    ConfID = model.Conference.ConfID,
                    Tags = model.ConferenceTag
                });

                await context.SaveChangesAsync();
                return RedirectToAction("Index", "Conference");

            }
            return View(model);
        }
    }
}
