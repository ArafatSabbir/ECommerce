using NecessaryDrugs.Core.Entities;
using NecessaryDrugs.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecessaryDrugs.Core.Services
{
    public class MedicineService : IMedicineService
    {
        private IMedicineStoreUnitOfWork _medicineStoreUnitOfWork;
        public MedicineService(IMedicineStoreUnitOfWork medicineStoreUnitOfWork)
        {
            _medicineStoreUnitOfWork = medicineStoreUnitOfWork;
        }

        public void AddANewMedicine(Medicine medicine)
        {
            if (medicine == null || string.IsNullOrWhiteSpace(medicine.Name))
            {
                throw new InvalidOperationException("Medicine name is missing");
            }
            else
            {
                _medicineStoreUnitOfWork.MedicineRepository.Add(medicine);
                _medicineStoreUnitOfWork.Save();
            }
        }

        public void AddMedicineCategory(int CateId, Medicine medicine)
        {
            Category category = GetCategoryById(CateId);
            _medicineStoreUnitOfWork.MedicineCategoryRepository.Add(new MedicineCategory
            {
                CategoryId = CateId,
                Category = category,
                MedicineId = medicine.Id,
                Medicine = medicine
            });
            _medicineStoreUnitOfWork.Save();

        }
        public Category GetCategoryById(int catId)
        {
            return _medicineStoreUnitOfWork.CategoryRepository.GetById(catId);
        }
        public IEnumerable<Category> GetAllCategories()
        {
            return _medicineStoreUnitOfWork.CategoryRepository.GetAll(); ;
        }

        public (IList<Medicine> records, int total, int totalDisplay) GetMedicines(int pageIndex, int pageSize, string searchText, string sortText)
        {
            return _medicineStoreUnitOfWork.MedicineRepository.GetDynamic(x => x.Name.Contains(searchText),
                sortText,
                "Categories,PriceDiscount,Image",
                pageIndex,
                pageSize,
                true
                );
        }

        public string GetCategoryListAsStringForAMedicine(IList<MedicineCategory> medicineCategories)
        {
            string allCategoryName="";
            foreach(MedicineCategory medicineCategory in medicineCategories)
            {
                var category=_medicineStoreUnitOfWork.CategoryRepository.GetById(medicineCategory.CategoryId);
                allCategoryName = allCategoryName +" , "+ category.Name;
            }
            return allCategoryName.TrimStart(' ',',');
        }

        public Medicine GetMedicine(int id)
        {
            return _medicineStoreUnitOfWork.MedicineRepository.GetByIdWithIncludeProperty(x => x.Id == id, "Categories,PriceDiscount,Image");
        }
        public IEnumerable<Category> GetCategoryListForAMedicine(IList<MedicineCategory> medicineCategories)
        {

            var categoryList =new List<Category>(); 
            foreach (MedicineCategory medicineCategory in medicineCategories)
            {
                var category = _medicineStoreUnitOfWork.CategoryRepository.GetById(medicineCategory.CategoryId);
                categoryList.Add(category);
            }
            return categoryList;
        }

        public void SaveInDbContext()
        {
            _medicineStoreUnitOfWork.Save();
        }

        public void DeleteMedicine(int id)
        {
            _medicineStoreUnitOfWork.MedicineRepository.Remove(id);
            _medicineStoreUnitOfWork.Save();
        }

        public void EditMedicine(Medicine medicine)
        {
            var oldMedicine= GetMedicine(medicine.Id);
            oldMedicine.Name = medicine.Name;
            oldMedicine.Description = medicine.Description;
            oldMedicine.Price = medicine.Price;
            oldMedicine.PriceDiscount = medicine.PriceDiscount;
            if (medicine.Image.Url!= null)
            {
                oldMedicine.Image = medicine.Image;
            }
            _medicineStoreUnitOfWork.Save();
        }

        public string GetDiscountAsString(Discount priceDiscount)
        {
            if(priceDiscount.GetType().Name== "FixedAmountDiscount")
            {
                return priceDiscount.Amount.ToString();
            }
            else
            {
                return priceDiscount.Amount.ToString() + " % ";
            }
        }
        public IEnumerable<Medicine> GetAllMedicine()
        {
            return _medicineStoreUnitOfWork.MedicineRepository.GetAll();
        }

    }
}
