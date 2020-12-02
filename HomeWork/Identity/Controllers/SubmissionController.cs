using Identity.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    public class SubmissionController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        public SubmissionController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        [Authorize(Roles = "Admin,Chair")]
        public async Task<IActionResult> Index(int id)
        {
            using var context = new IdentityContext();
            var activeuser = await _userManager.FindByNameAsync(User.Identity.Name);
            var role = await _userManager.GetRolesAsync(activeuser);

            if (role.Contains("Admin"))
            {
                return View(context.Submissions.Where(I => I.ConfId == id).ToList());
            }
            else if (role.Contains("Chair"))
            {
                return View(context.Submissions.Where(I => I.IsActive == true && I.ConfId == id).ToList());
            }
            else
            {
                var conf = context.Conferences.Where(I => I.CreateUserId == activeuser.Id && I.ConfID == id).First();
                var submission = context.Submissions.Where(I => I.ConfId == conf.ConfID);
                return View(submission);
            }

        }

        public IActionResult Delete(int id, int confId)
        {
            using var context = new IdentityContext();
            var conference = context.Submissions.Find(id);
            context.Submissions.Remove(conference);
            context.SaveChanges();
            return RedirectToAction("Index", new { id = confId });
        }

        public IActionResult Edit(int id)
        {
            using var context = new IdentityContext();
            var submission = context.Submissions.Find(id);
            return View("Edit", submission);
        }

        [HttpPost]
        public IActionResult Edit(Submissions model)
        {
            if (ModelState.IsValid)
            {
                using var context = new IdentityContext();
                var usubmission = context.Submissions.Find(model.SubmissionID);

                usubmission.IsActive = model.IsActive;
                usubmission.prevSubmissionID = model.prevSubmissionID;
                usubmission.Submission = model.Submission;


                context.Update(usubmission);
                context.SaveChanges();
                return RedirectToAction("Index", new { id = model.ConfId });
            }
            return View(model);
        }

    }
}