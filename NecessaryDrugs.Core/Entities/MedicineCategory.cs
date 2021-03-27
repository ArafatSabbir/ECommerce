using System;
using System.Collections.Generic;
using System.Text;

namespace NecessaryDrugs.Core.Entities
{
    public class MedicineCategory
    {
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
