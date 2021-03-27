using Autofac;
using NecessaryDrugs.Core.Entities;
using NecessaryDrugs.Core.Services;
using NecessaryDrugs.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NecessaryDrugs.Web.Areas.Admin.Models
{
    public class MedicineViewModel
    {
        private IMedicineService _medicineService;

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
        public IList<MedicineImage> Images { get; set; }
        public Discount PriceDiscount { get; set; }
        public MedicineViewModel()
        {
            _medicineService = Startup.AutofacContainer.Resolve<IMedicineService>();
        }
        public MedicineViewModel(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        public object GetMedicines(DataTablesAjaxRequestModel tableModel)
        {
            var data = _medicineService.GetMedicines(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "Name","Image", "Description","Categories", "Price", "PriceDiscount.Amount" ,"Action"}));
            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.Name,
                            record.Image.Url,
                            record.Description,
                            _medicineService.GetCategoryListAsStringForAMedicine(record.Categories),
                            record.Price.ToString(),
                            _medicineService.GetDiscountAsString(record.PriceDiscount),
                            //record.PriceDiscount.GetType().Name,
                            //record.PriceDiscount.Amount.ToString(),
                            record.Id.ToString()
                        }
                    ).ToList()
            };
        }
    }
}
