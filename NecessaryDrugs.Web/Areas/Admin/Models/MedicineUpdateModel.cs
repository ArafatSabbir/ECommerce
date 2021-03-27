using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using NecessaryDrugs.Core.Entities;
using NecessaryDrugs.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NecessaryDrugs.Web.Areas.Admin.Models
{
    public class MedicineUpdateModel : BaseModel
    {
        private IMedicineService _medicineService;
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public string Category { get; set; }
        public IList<SelectListItem> Categories { get; set; }
        public IList<string> CategoriesId { get; set; }
        public Discount Discount { get; set; }
        internal IEnumerable<Category> GetAllCategory()
        {
            return _medicineService.GetAllCategories();
        }
        IList<MedicineCategory> SelectedCategories { get; set; }
        public MedicineImage Image2 { get; set; }

        public IFormFile Image { get; set; }
        public string DisountType { get; set; }
        public double AmountOrPercentage { get; set; }
        public string ReturnUrl { get; set; }
        public string Url { get; set; }
        public List<MedicineCategoryViewModel> medCatModel { get; set; }
        public MedicineUpdateModel()
        {
            _medicineService = Startup.AutofacContainer.Resolve<IMedicineService>(); 
        }
        public MedicineUpdateModel(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }
        internal void AddNewMedicine()
        {
            if (DisountType == "FixedAmountDiscount")
            {
                Discount = new FixedAmountDiscount { Amount = AmountOrPercentage };
            }
            else
            {
                Discount = new PercentageDiscount { Amount = AmountOrPercentage };
            }
            if (Url == null)
            {
                Url = "no-image.png";
            }
            try
            {
                var medicine = new Medicine
                {
                    Name = Name,
                    Description = Description,
                    Image = new MedicineImage { Url = Url },
                    Price = Price,
                    PriceDiscount = Discount
                };
                _medicineService.AddANewMedicine(medicine);
                foreach (string s in CategoriesId)
                {
                    int CatId = Convert.ToInt32(s);
                    _medicineService.AddMedicineCategory(CatId, medicine);
                }
                Notification = new NotificationModel("Success!",
                    "Category added successfully.",
                    Notificationtype.Success);
            }
            catch (InvalidOperationException iex)
            {
                Notification = new NotificationModel("Failed!",
                    "Failed to add category, please provide valid name.",
                    Notificationtype.Fail);
            }
        }

        internal void Load(int id)
        {
            var medicine = _medicineService.GetMedicine(id);
            if (medicine != null)
            {
                Id = medicine.Id;
                Name = medicine.Name;
                Description = medicine.Description;
                Price = medicine.Price;
                Url = medicine.Image.Url;
                AmountOrPercentage = medicine.PriceDiscount.Amount;
                DisountType = medicine.PriceDiscount.GetType().Name;
            }
        }

        internal void EditMedicine()
        {
            if (DisountType == "FixedAmountDiscount")
            {
                Discount = new FixedAmountDiscount { Amount = AmountOrPercentage };
            }
            else
            {
                Discount = new PercentageDiscount { Amount = AmountOrPercentage };
            }
            try
            {
                _medicineService.EditMedicine(new Medicine
                {
                    Id=Id,
                    Name = Name,
                    Description = Description,
                    Image = new MedicineImage { Url = Url },
                    Price = Price,
                    PriceDiscount = Discount
                });
                Notification = new NotificationModel("Success!",
                    "Medicine edited successfully.",
                    Notificationtype.Success);
            }
            catch (InvalidOperationException iex)
            {
                Notification = new NotificationModel("Failed!",
                    "Failed to edit medicine, please provide valid name.",
                    Notificationtype.Fail);
            }
        }

        internal void Delete(int id)
        {
            _medicineService.DeleteMedicine(id);
        }
    }
}
