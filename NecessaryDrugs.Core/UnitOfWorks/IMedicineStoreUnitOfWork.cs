
using NecessaryDrugs.Core.Contexts;
using NecessaryDrugs.Core.Repositories;
using NecessaryDrugs.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecessaryDrugs.Core.UnitOfWorks
{
    public interface IMedicineStoreUnitOfWork : IUnitOfWork<MedicineStoreContext>
    {
        IMedicineRepository MedicineRepository { get; set; }
        ICategoryRepository CategoryRepository { get; set; }
        IStockRepository StockRepository { get; set; }
        IMedicineCategoryRepository MedicineCategoryRepository { get; set; }
        IOrderRepository OrderRepository { get; set; }
    }
}