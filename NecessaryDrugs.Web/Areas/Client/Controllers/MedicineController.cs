using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NecessaryDrugs.Web.Areas.Client.Models;
using Newtonsoft.Json;
using X.PagedList;

namespace NecessaryDrugs.Web.Areas.Client.Controllers
{
    [Area("Client")]
    public class MedicineController : Controller
    {
        //public IActionResult Index()
        //{
        //    var model = new MedicineViewModel();
        //    return View(model.GetMedicines());
        //}
        

        public IActionResult Index(int? page)//Add page parameter
        {
            var model = new MedicineViewModel();
            var pageNumber = page ?? 1; // if no page is specified, default to the first page (1)
            int pageSize = 6; // Get 25 students for each requested page.
            var onePageOfStudents = model.GetMedicines().ToPagedList(pageNumber, pageSize);
            return View(onePageOfStudents); // Send 25 students to the page.
        }
        

    }
}
