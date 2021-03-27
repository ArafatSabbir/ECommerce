using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NecessaryDrugs.Core.Entities
{
    public class Medicine
    {
        [Required]
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(1000)] 
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required(ErrorMessage ="Add an image of the medicine.")]
        public MedicineImage Image { get; set; }
        public IList<MedicineCategory> Categories { get; set; }
        public Discount PriceDiscount { get; set; }
        public Stock Stock { get; set; }
        public IList<OrderItem> OrderedMedicines { get; set; }
        //public Order Order { get; set; }
        //public Purchase Purchase { get; set; }
    }
}
