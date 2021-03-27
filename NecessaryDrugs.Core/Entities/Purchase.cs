using System;
using System.Collections.Generic;
using System.Text;

namespace NecessaryDrugs.Core.Entities
{
    public class Purchase
    {
        public int Id { get; set; }
        public IList<PurchaseItem> PurchasedMedicines { get; set; }
        public Supplier Supplier { get; set; }
        public string SupplierId { get; set; }
        public string QuantitiesListAsString { get; set; }
        public double TotalBill { get; set; }
    }
}
