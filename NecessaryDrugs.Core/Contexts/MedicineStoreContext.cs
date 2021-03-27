using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NecessaryDrugs.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecessaryDrugs.Core.Contexts
{
    public class MedicineStoreContext : IdentityDbContext<NormalUser, ApplicationRole, string, IdentityUserClaim<string>,
    ApplicationUserRole, IdentityUserLogin<string>,
    IdentityRoleClaim<string>, IdentityUserToken<string>>,IMedicineStoreContext
    {
        private string _connectionString;
        private string _migrationAssemblyName;

        public MedicineStoreContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(
                    _connectionString,
                    m => m.MigrationsAssembly(_migrationAssemblyName));
            }

            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Medicine>()
                .HasOne(p => p.Image)
                .WithOne(i => i.Medicine);

            builder.Entity<Medicine>()
                .HasOne(p => p.PriceDiscount)
                .WithOne(d => d.Medicine);

            builder.Entity<MedicineCategory>()
                .HasKey(pc => new { pc.MedicineId, pc.CategoryId });
            builder.Entity<MedicineCategory>()
                .HasOne(pc => pc.Medicine)
                .WithMany(p => p.Categories)
                .HasForeignKey(pc => pc.MedicineId);
            builder.Entity<MedicineCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.Categories)
                .HasForeignKey(pc => pc.CategoryId);

            builder.Entity<Medicine>()
                .HasOne(m => m.Stock)
                .WithOne(s => s.Medicine);
            builder.Entity<Stock>()
                .HasOne(s => s.Medicine)
                .WithOne(m => m.Stock);
            builder.Entity<Supplier>()
                .HasOne(s => s.User)
                .WithOne(n => n.Supplier);

            builder.Entity<OrderItem>()
                .HasKey(oi => new { oi.MedicineId, oi.OrderId });
            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderedMedicines)
                .HasForeignKey(oi=>oi.OrderId);
            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Medicine)
                .WithMany(o => o.OrderedMedicines)
                .HasForeignKey(oi=>oi.MedicineId);

            //builder.Entity<Purchase>()
            //    .HasMany(p => p.Medicines)
            //    .WithOne(m => m.Purchase);


            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });


            base.OnModelCreating(builder);
        }

        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<MedicineImage> MedicineImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MedicineCategory> MedicineCategories { get; set; }
        public DbSet<FixedAmountDiscount> FixedAmountDiscounts { get; set; }
        public DbSet<PercentageDiscount> PercentageDiscounts { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}
