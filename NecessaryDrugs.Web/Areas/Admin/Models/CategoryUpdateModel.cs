using Autofac;
using Autofac.Extensions.DependencyInjection;
using NecessaryDrugs.Core.Entities;
using NecessaryDrugs.Core.Services;
using NecessaryDrugs.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NecessaryDrugs.Web.Areas.Admin.Models
{
    public class CategoryUpdateModel : BaseModel
    {
        ICategoryService _categoryService;
        public int Id { get; set; }
        public string Name { get; set; }

        public CategoryUpdateModel()
        {
            _categoryService = Startup.AutofacContainer.Resolve<ICategoryService>();
        }
        public CategoryUpdateModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        

        internal void AddCaregory()
        {
            try
            {

                _categoryService.AddANewCategory(new Category
                {
                    Name = this.Name
                });
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
            catch (Exception ex)
            {
                Notification = new NotificationModel("Failed!",
                    "Failed to add category, please try again.",
                    Notificationtype.Fail);
            }

        }

        internal void EditCaregory()
        {
            try
            {
                _categoryService.EditCategory(new Category
                {
                    Id = this.Id,
                    Name = this.Name
                });
                Notification = new NotificationModel("Success!",
                    "Category edited successfully.",
                    Notificationtype.Success);
            }
            catch (InvalidOperationException iex)
            {
                Notification = new NotificationModel("Failed!",
                    "Failed to edit category, please provide valid name.",
                    Notificationtype.Fail);
            }
            catch (Exception ex)
            {
                Notification = new NotificationModel("Failed!",
                    "Failed to edit category, please try again.",
                    Notificationtype.Fail);
            }
        }

        internal void Delete(int id)
        {
            _categoryService.DeleteCategory(id);
        }

        public void Load(int id)
        {
            var category = _categoryService.GetCategoy(id);
            if (category != null)
            {
                Id = category.Id;
                Name = category.Name;
            }
        }

    }

}
