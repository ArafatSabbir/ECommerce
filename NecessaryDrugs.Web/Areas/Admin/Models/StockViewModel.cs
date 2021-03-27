using Autofac;
using NecessaryDrugs.Core.Entities;
using NecessaryDrugs.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace NecessaryDrugs.Web.Areas.Admin.Models
{
    public class StockViewModel
    {
        public int Id { get; set; }
        [DisplayName("Medicine Name")]
        public string MedicineName { get; set; }
        public Medicine Medicine { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public string Description { get; set; }
        private IStockService _StockService;
        public StockViewModel()
        {
            _StockService = Startup.AutofacContainer.Resolve<IStockService>();
        }
        public StockViewModel(IStockService StockService)
        {
            _StockService = StockService;
        }
        public IEnumerable<StockViewModel> GetStocks()
        {
            var allData = _StockService.GetAllStocks();
            var StockModelList = new List<StockViewModel>();
            foreach (var stock in allData)
            {
                var medicine = _StockService.GetMedicine(stock.MedicineId);
                StockModelList.Add(new StockViewModel
                {
                    Id = stock.Id,
                    MedicineName = medicine.Name,
                    Quantity = stock.Quantity,
                    TotalPrice = stock.TotalPrice,
                    Description = stock.Description
                });
            }
            return StockModelList;
        }
    }
}
