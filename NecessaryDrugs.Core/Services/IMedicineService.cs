using NecessaryDrugs.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecessaryDrugs.Core.Services
{
    public interface IMedicineService
    {
        (IList<Medicine> records, int total, int totalDisplay) GetMedicines(int pageIndex, int pageSize, string searchText, string sortText);
        void AddANewMedicine(Medicine medicine);
        void AddMedicineCategory(int CateId, Medicine medicine);
        string GetCategoryListAsStringForAMedicine(IList<MedicineCategory> medicineCategories);
        IEnumerable<Medicine> GetAllMedicine();
        IEnumerable<Category> GetCategoryListForAMedicine(IList<MedicineCategory> medicineCategories);
        IEnumerable<Category> GetAllCategories();
        Medicine GetMedicine(int id);
        void SaveInDbContext();
        void DeleteMedicine(int id);
        void EditMedicine(Medicine medicine);
        string GetDiscountAsString(Discount priceDiscount);
    }
}
