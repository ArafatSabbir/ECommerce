using Autofac;
using Microsoft.AspNetCore.Mvc.Rendering;
using NecessaryDrugs.Core.Entities;
using NecessaryDrugs.Core.Services;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NecessaryDrugs.Web.Areas.Admin.Models
{
    public class StockUpdateModel : BaseModel
    {
        IStockService _stockService;
        public int Id { get; set; }
        //[Required(ErrorMessage ="This field is required")]
        public int MedicineId { get; set; }
        //public Medicine Medicine { get; set; }
        //[Required(ErrorMessage = "This field is required")]
        //public IList<SelectListItem> Medicines { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public string Description { get; set; }
        public StockUpdateModel()
        {
            _stockService = Startup.AutofacContainer.Resolve<IStockService>();
        }
        public StockUpdateModel(IStockService stockService)
        {
            _stockService = stockService;
        }

        internal void Load(int id)
        {
            var stock = _stockService.GetStock(id);
            if (stock != null)
            {
                Id = stock.Id;
                MedicineId = stock.MedicineId;
                Quantity = stock.Quantity;
                Description = stock.Description;
            }
        }

        internal void AddStock()
        {
            try
            {
                Medicine medicine=_stockService.GetMedicine(MedicineId);
                _stockService.AddANewStock(new Stock
                {
                    MedicineId = medicine.Id,
                    Quantity=Quantity,
                    TotalPrice=Quantity*medicine.Price,
                    Description=Description
                });
                Notification = new NotificationModel("Success!",
                    "Stock added successfully.",
                    Notificationtype.Success);
            }
            catch (InvalidOperationException iex)
            {
                Notification = new NotificationModel("Failed!",
                    "Failed to add Stock, please provide valid medicine name.",
                    Notificationtype.Fail);
            }
            catch (Exception ex)
            {
                Notification = new NotificationModel("Failed!",
                    "Failed to add Stock, please try again.",
                    Notificationtype.Fail);
            }
        }

        internal void Delete(int id)
        {
            _stockService.DeleteStock(id);
        }

        internal void UpdateStock(int id)
        {
            try
            {
                var oldStock = _stockService.GetStock(id);
                oldStock.MedicineId = this.MedicineId;
                oldStock.Quantity = Quantity;
                oldStock.Description = Description;
                Notification = new NotificationModel("Success!",
                    "Stock added successfully.",
                    Notificationtype.Success);
            }
            catch (InvalidOperationException iex)
            {
                Notification = new NotificationModel("Failed!",
                    "Failed to add Stock, please provide valid name.",
                    Notificationtype.Fail);
            }
            catch (Exception ex)
            {
                Notification = new NotificationModel("Failed!",
                    "Failed to add Stock, please try again.",
                    Notificationtype.Fail);
            }
        }

        //public IEnumerable<StockViewModel> GetAllStocks()
        //{
        //    var allData = _stockService.GetAllStocks();
        //    var StockModelList = new List<StockViewModel>();
        //    foreach (var stock in allData)
        //    {
        //        StockModelList.Add(new StockViewModel
        //        {
        //            Id = stock.Id,
        //            MedicineName = stock.Medicine.Name,
        //            Quantity = stock.Quantity,
        //            TotalPrice = stock.TotalPrice,
        //            Description = stock.Description
        //        });
        //    }
        //    return StockModelList;
        //}
    }
}
