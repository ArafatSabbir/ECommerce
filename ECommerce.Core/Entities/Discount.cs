
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Core.Entities
{
    public abstract class Discount
    {
        public double Amount { get; set; }
        public abstract double CalculateDiscount();

    }
}
