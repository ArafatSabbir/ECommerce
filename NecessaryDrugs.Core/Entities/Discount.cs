using System;
using System.Collections.Generic;
using System.Text;

namespace NecessaryDrugs.Core.Entities
{
    public abstract class Discount
    {
        public int Id { get; set; }

        private double _amount;
        public double Amount
        {
            get { return _amount; }
            set
            {
                if (value >= 0)
                {
                    _amount = value;
                }
                else
                {
                    throw new InvalidOperationException("Discount can't be negative");
                }
            }
        }
        public Medicine Medicine { get; set; }
        public int MedicineId { get; set; }
        public abstract double CalculatePriceAfterDiscount(double price);
    }
}
