using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NecessaryDrugs.Core.Entities
{
    public class Category
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public IList<MedicineCategory> Categories { get; set; }
    }
}
