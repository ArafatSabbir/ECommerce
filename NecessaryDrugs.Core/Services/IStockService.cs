using NecessaryDrugs.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecessaryDrugs.Core.Services
{
    public interface IStockService
    {
        IEnumerable<Stock> GetAllStocks();
        void AddANewStock(Stock stock);
        Stock GetStock(int id);
        void EditStock(Stock stock);
        void DeleteStock(int id);
        Medicine GetMedicine(int medicineId);
    }
}
