using NecessaryDrugs.Core.Entities;
using NecessaryDrugs.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;

namespace NecessaryDrugs.Core.Services
{
    public class StockService : IStockService
    {
        private IMedicineStoreUnitOfWork _medicineStoreUnitOfWork;
        public StockService(IMedicineStoreUnitOfWork medicineStoreUnitOfWork)
        {
            _medicineStoreUnitOfWork = medicineStoreUnitOfWork;
        }

        public void AddANewStock(Stock stock)
        {
            if (stock == null)
            {
                throw new InvalidOperationException("Input data is missing");
            }
            else
            {
                _medicineStoreUnitOfWork.StockRepository.Add(stock);
                _medicineStoreUnitOfWork.Save();
            }
        }

        public void DeleteStock(int id)
        {
            _medicineStoreUnitOfWork.StockRepository.Remove(id);
            _medicineStoreUnitOfWork.Save();
        }

        public void EditStock(Stock stock)
        {
            var oldStock = _medicineStoreUnitOfWork.StockRepository.GetById(stock.Id);
            oldStock.MedicineId = stock.MedicineId;
            oldStock.Quantity = stock.Quantity;
            oldStock.Description = stock.Description;
            _medicineStoreUnitOfWork.Save();
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            return _medicineStoreUnitOfWork.StockRepository.GetAll();
            //return _medicineStoreUnitOfWork.StockRepository.Get(null, y => y.OrderBy("Medicne.Name"),"",true);

        }

        public Medicine GetMedicine(int medicineId)
        {
            return _medicineStoreUnitOfWork.MedicineRepository.GetById(medicineId);
        }

        public Stock GetStock(int id)
        {
            return _medicineStoreUnitOfWork.StockRepository.GetById(id);
        }
    }
}
