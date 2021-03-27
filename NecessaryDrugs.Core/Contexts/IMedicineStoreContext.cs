using Microsoft.EntityFrameworkCore;
using NecessaryDrugs.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecessaryDrugs.Core.Contexts
{
    public interface IMedicineStoreContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Medicine> Medicines { get; set; }
        DbSet<MedicineCategory> MedicineCategories { get; set; }
        DbSet<MedicineImage> MedicineImages { get; set; }
        DbSet<FixedAmountDiscount> FixedAmountDiscounts { get; set; }
        DbSet<PercentageDiscount> PercentageDiscounts { get; set; }
        DbSet<Stock> Stocks { get; set; }
        DbSet<NormalUser> Users { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }
        DbSet<Supplier> Suppliers { get; set; }
    }
}
