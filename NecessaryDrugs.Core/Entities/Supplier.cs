using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NecessaryDrugs.Core.Entities
{
    public class Supplier
    {
        public string CompanyName { get; set; }
        public string CompanyAdress { get; set; }
        public NormalUser User { get; set; }
        [Key]
        public string UserId { get; set; }
    }
}
