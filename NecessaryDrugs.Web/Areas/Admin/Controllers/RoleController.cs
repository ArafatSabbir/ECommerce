using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NecessaryDrugs.Core.Entities;
using NecessaryDrugs.Web.Areas.Admin.Models;
using NecessaryDrugs.Web.Models;

namespace NecessaryDrugs.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly UserManager<NormalUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<RoleController> _logger;

        public RoleController(
            UserManager<NormalUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            ILogger<RoleController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new RoleViewModel();
            return View(model);
        }

        public IActionResult GetRoles()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = new RoleViewModel();
            var data = model.GetRoles(tableModel);
            return Json(data);
        }

        //[Authorize(Policy = "InternalOfficials")]
        public IActionResult Add()
        {
            var model = new RoleUpdateModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(RoleUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                await model.AddNewRole();
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(string id)
        {
            var model = new RoleUpdateModel();
            model.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
