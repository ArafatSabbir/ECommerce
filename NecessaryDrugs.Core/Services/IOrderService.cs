using NecessaryDrugs.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecessaryDrugs.Core.Services
{
    public interface IOrderService
    {
        Medicine GetMedicine(int id);
        void AddAnOrder(Order order);
        object GetAllStocks();
    }
}
