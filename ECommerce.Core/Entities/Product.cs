using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string Price { get; set; }

    }
}
