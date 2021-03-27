using Autofac;
using Microsoft.AspNetCore.Identity;
using NecessaryDrugs.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NecessaryDrugs.Web.Areas.Admin.Models
{
    public class RoleUpdateModel : BaseModel
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleUpdateModel()
        {
            _roleManager = Startup.AutofacContainer.Resolve<RoleManager<ApplicationRole>>();
        }

        internal async Task AddNewRole()
        {
            await _roleManager.CreateAsync(new ApplicationRole() { Name = this.RoleName });
            Notification = new NotificationModel("Success", "Role Added", Notificationtype.Success);
        }

        internal async void Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(role);
        }
    }
}
