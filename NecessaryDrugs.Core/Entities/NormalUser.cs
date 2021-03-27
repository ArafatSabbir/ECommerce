using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text;

namespace NecessaryDrugs.Core.Entities
{
    public class NormalUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public string ContatctNo { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
        public Supplier Supplier { get; internal set; }
    }
}
