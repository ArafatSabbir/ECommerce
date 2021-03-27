using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NecessaryDrugs.Web.Areas.Admin.Models;

namespace NecessaryDrugs.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StockController : Controller
    {

        public IActionResult Index()
        {
            var model = new StockViewModel();
            return View(model.GetStocks());
        }
        [HttpGet]
        public IActionResult AddOrEdit(int id = 0)
        {
            var model = new StockUpdateModel();

            if (id == 0)
                return View(model);
            else
            {
                model.Load(id);
                return View(model);
            }    
        }
        [HttpPost]
        public IActionResult AddOrEdit(int id,  StockUpdateModel model)
        {
            var viewModel = new StockViewModel();
            if (ModelState.IsValid)
            {
                //Add
                if (id == 0)
                {
                    model.AddStock();

                }
                //Update
                else
                {
                    model.UpdateStock(id);
                }
                return Json(new { isValid = true, html = Helper<StockController>.RenderRazorViewToString(this, "_ViewAll", viewModel.GetStocks()) });
            }
            return Json(new { isValid = false, html = Helper<StockController>.RenderRazorViewToString(this, "AddOrEdit", model) });
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var model = new StockUpdateModel();
            var viewModel = new StockViewModel();
            model.Delete(id);
            return Json(new { html = Helper<StockController>.RenderRazorViewToString(this, "_ViewAll", viewModel.GetStocks()) });
        }
    }
}
