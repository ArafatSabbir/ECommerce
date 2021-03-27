using NecessaryDrugs.Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NecessaryDrugs.Core.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public IList<OrderItem> OrderedMedicines { get; set; }
        public string QuantitiesListAsString { get; set; }
        public double TotalPrice { get; set; }
        public string UserId { get; set; }
        public DateTime Orderdate { get; set; }
        public string DeliveryStatus { get; set; }
        public string DelivaryAdress { get; set; }
        public string ContactNo { get; set; }
        public DateTime DaliveryDate { get; set; }
        public string PaymentType { get; set; }
        public string PaymentTransactionID { get; set; }
    }
}
