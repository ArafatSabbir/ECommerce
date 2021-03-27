using NecessaryDrugs.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecessaryDrugs.Core.Services
{
    public interface ICategoryService
    {
        void AddANewCategory(Category category);
        (IList<Category> data, int total, int totalDisplay) GetCategories(int pageIndex, int pageSize, string searchText,string sortText);
        Category GetCategoy(int id);
        void EditCategory(Category category);
        void DeleteCategory(int id);
    }
}
