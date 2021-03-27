using System;
using System.Collections.Generic;
using System.Text;

namespace NecessaryDrugs.Core.Entities
{
    public class OrderItem
    {
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
