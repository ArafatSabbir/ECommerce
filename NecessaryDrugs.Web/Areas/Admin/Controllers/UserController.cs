using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using NecessaryDrugs.Core.Entities;
using NecessaryDrugs.Web.Areas.Admin.Models;
using NecessaryDrugs.Web.Models;

namespace NecessaryDrugs.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly SignInManager<NormalUser> _signInManager;
        private readonly UserManager<NormalUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<UserModel> _logger;
        private readonly IEmailSender _emailSender;

        public UserController(
            UserManager<NormalUser> userManager,
            SignInManager<NormalUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            ILogger<UserModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _emailSender = emailSender;
        }
        public async Task<IActionResult> Index()
        {
            //var data = GetUsers();
            var model = new UserModel();
            var users = await model.GetUsers();
            return View(users);
        }
       
        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var model = new UserModel();
            model.Delete(id);
            return RedirectToAction("Index");
            //Note: After clicking delete, the operation will be done, but the page will not be refreshed.

            //return Json(new { html = Helper<UserController>.RenderRazorViewToString(this, "Index", model.GetUsers() ) });
            //return Json(new { html = Helper<UserController>.RenderRazorViewToString(this, "Index", model.GetUsers()) });
        }

        [HttpGet]
        public async Task<IActionResult> Manageroles(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRolesViewModel>();

            foreach (var role in _roleManager.Roles.ToList())
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }

                model.Add(userRolesViewModel);
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Manageroles(List<UserRolesViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }

            result = await _userManager.AddToRolesAsync(user,
                model.Where(x => x.IsSelected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }

            return RedirectToAction("Index", new { Id = userId });
        }

        //[HttpGet]
        //public ActionResult Edit(string id)
        //{
        //    var model = new UserModel();
        //    model.Load(id);
        //    return View(model);
        //}
        //[HttpPost]
        //public  IActionResult Edit(UserModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        model.EditUser();
        //    }
        //    return RedirectToAction("Index");
        //}

        //public IActionResult GetUsers()
        //{
        //    var tableModel = new DataTablesAjaxRequestModel(Request);
        //    var model = new UserModel();
        //    var data = model.GetAllUsers(tableModel);
        //    return Json(data);
        //}
    }
}
