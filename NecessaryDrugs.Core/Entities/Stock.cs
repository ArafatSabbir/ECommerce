using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace NecessaryDrugs.Core.Entities
{
    public class Stock
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public string Description { get; set; }
    }
}
