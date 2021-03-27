using Autofac;
using NecessaryDrugs.Core.Services;
using NecessaryDrugs.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NecessaryDrugs.Web.Areas.Admin.Models
{
    public class CategoryViewModel : BaseModel
    {
        private ICategoryService _categoryService;
        public CategoryViewModel()
        {
            _categoryService = Startup.AutofacContainer.Resolve<ICategoryService>();
        }
        public CategoryViewModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public object GetCategories(DataTablesAjaxRequestModel tableModel)
        {
            var records = _categoryService.GetCategories(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "Name"}));

            return new
            {
                recordsTotal = records.total,
                recordsFiltered = records.totalDisplay,
                data = (from record in records.data
                        select new string[]
                        {
                            record.Name,
                            record.Id.ToString()
                        }
                    ).ToArray()

            };
        }
    }
}
