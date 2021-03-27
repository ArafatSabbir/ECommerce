using Autofac;
using NecessaryDrugs.Core.Entities;
using NecessaryDrugs.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NecessaryDrugs.Web.Areas.Admin.Models
{
    public class MedicineCategoryViewModel
    {
        private IMedicineService _medicineService;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsSelected { get; set; }
        public MedicineCategoryViewModel()
        {
            _medicineService = Startup.AutofacContainer.Resolve<IMedicineService>();
        }
        public MedicineCategoryViewModel(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        public List<MedicineCategoryViewModel> GetSelectedMedicineCategories(int medicineId)
        {
            var medicine=_medicineService.GetMedicine(medicineId);
            var medicineCategories = medicine.Categories;
            var selectedCategories = _medicineService.GetCategoryListForAMedicine(medicineCategories);
            var allCategories = _medicineService.GetAllCategories();
            var modelList = new List<MedicineCategoryViewModel>();
            foreach(var category in allCategories)
            {
                var model = new MedicineCategoryViewModel
                {
                    CategoryId = category.Id,
                    CategoryName = category.Name
                };

                if (selectedCategories.Contains(category))
                {
                    model.IsSelected = true;
                }
                else
                {
                    model.IsSelected = false;
                }
                modelList.Add(model);
            }
            //int count = medicineCategories.Count;
            //for (int i = 0; i < count;i++)
            //{
            //    medicine.Categories.RemoveAt(0);
            //    _medicineService.SaveInDbContext();
            //}
            //foreach(var medCategory in medCategoriesStatic)
            //{
            //    medicine.Categories.Remove(medCategory);
            //}
            return modelList;
        }
        public void SetSelectedCategory(List<MedicineCategoryViewModel> modelList, int medicineId)
        {
            var medicine = _medicineService.GetMedicine(medicineId);
            int count = medicine.Categories.Count;
            for (int i = 0; i < count; i++)
            {
                medicine.Categories.RemoveAt(0);
                _medicineService.SaveInDbContext();
            }
            foreach (var model in modelList)
            {
                if (model.IsSelected == true)
                {
                    _medicineService.AddMedicineCategory(model.CategoryId, medicine);
                }
            }
        }
    }
}
