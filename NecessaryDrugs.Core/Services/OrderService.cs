using NecessaryDrugs.Core.Entities;
using NecessaryDrugs.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecessaryDrugs.Core.Services
{
    public class OrderService : IOrderService
    {
        private IMedicineStoreUnitOfWork _medicineStoreUnitOfWork;
        public OrderService(IMedicineStoreUnitOfWork medicineStoreUnitOfWork)
        {
            _medicineStoreUnitOfWork = medicineStoreUnitOfWork;
        }

        public void AddAnOrder(Order order)
        {
            _medicineStoreUnitOfWork.OrderRepository.Add(order);
            _medicineStoreUnitOfWork.Save();
        }

        public object GetAllStocks()
        {
            throw new NotImplementedException();
        }

        public Medicine GetMedicine(int id)
        {
            return _medicineStoreUnitOfWork.MedicineRepository.GetByIdWithIncludeProperty(x => x.Id == id, "Categories,PriceDiscount,Image");
        }
    }
}
