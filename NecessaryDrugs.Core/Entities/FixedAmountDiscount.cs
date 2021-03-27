using System;
using System.Collections.Generic;
using System.Text;

namespace NecessaryDrugs.Core.Entities
{
    public class FixedAmountDiscount : Discount
    {
        public override double CalculatePriceAfterDiscount(double price)
        {
            if (Amount > price)
            {
                return 0;
            }
            else
            {
                return price - Amount;
            }
        }
    }
}
