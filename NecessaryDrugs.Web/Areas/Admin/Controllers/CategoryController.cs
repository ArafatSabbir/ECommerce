using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NecessaryDrugs.Web.Areas.Admin.Models;
using NecessaryDrugs.Web.Models;

namespace NecessaryDrugs.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            var model=new CategoryUpdateModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(CategoryUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                model.AddCaregory();
            }
            return View(model);
        }
        public IActionResult GetCategories()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = new CategoryViewModel();
            var data = model.GetCategories(tableModel);
            return Json(data);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = new CategoryUpdateModel();
            model.Load(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(CategoryUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                model.EditCaregory();
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var model = new CategoryUpdateModel();
            model.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
