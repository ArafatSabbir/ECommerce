using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecessaryDrugs.Core.Entities
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual NormalUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
