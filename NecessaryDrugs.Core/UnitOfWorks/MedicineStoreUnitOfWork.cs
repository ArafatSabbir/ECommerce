using NecessaryDrugs.Core.Contexts;
using NecessaryDrugs.Core.Repositories;
using NecessaryDrugs.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecessaryDrugs.Core.UnitOfWorks
{
    public class MedicineStoreUnitOfWork : UnitOfWork<MedicineStoreContext>,IMedicineStoreUnitOfWork
    {
        public IMedicineRepository MedicineRepository { get; set; }
        public ICategoryRepository CategoryRepository { get; set; }
        public IStockRepository StockRepository { get; set; }
        public IOrderRepository OrderRepository { get; set; }
        public IMedicineCategoryRepository MedicineCategoryRepository { get; set; }

        public MedicineStoreUnitOfWork(string connectionString, string migrationAssemblyName)
            : base(connectionString, migrationAssemblyName)
        {
            MedicineRepository = new MedicineRepository(_dbContext);
            CategoryRepository = new CategoryRepository(_dbContext);
            MedicineCategoryRepository = new MedicineCategoryRepository(_dbContext);
            StockRepository = new StockRepository(_dbContext);
            OrderRepository = new OrderRepository(_dbContext);
        }
    }
}
