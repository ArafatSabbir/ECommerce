using Autofac;
using Microsoft.AspNetCore.Razor.Language;
using NecessaryDrugs.Core.Entities;
using NecessaryDrugs.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NecessaryDrugs.Web.Areas.Client.Models
{
    public class CartModel
    {
        IOrderService _orderService;
        public CartModel()
        {
            _orderService = Startup.AutofacContainer.Resolve<IOrderService>();
        }

        //public IEnumerable<CartModel> GetStocks()
        //    {
        //        var allData = _orderService.GetAllStocks();
        //        var OrderModelList = new List<CartModel>();
        //        foreach (var order in allData)
        //        {
        //            var medicine = _StockService.GetMedicine(stock.MedicineId);
        //            StockModelList.Add(new CartModel
        //            {
        //                OrderId = order.Id,
        //                TotalPrice = stock.Quantity,
        //                TotalPrice = stock.TotalPrice,
        //                Description = stock.Description
        //            });
        //        }
        //        return StockModelList;
        //    }
        

        public CartModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public int OrderId { get; set; }
        public int MedicineId { get; set; }
        public string MedImgUrl{get; set;}
        public string MedName { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public DateTime Orderdate { get; set; }
        
        public enum PaymentType
        {
            Cash,
            Card,
            cheque
        }
        public string PaymentTransactionID { get; set; }
        internal CartModel AddToCart(int id, int quantity)
        {
            Medicine medicine = _orderService.GetMedicine(id);
            var cart = new CartModel();
            cart.MedicineId = id;
            cart.MedName = medicine.Name;
            cart.MedImgUrl = medicine.Image.Url;
            cart.UnitPrice = medicine.Price;
            cart.Quantity = quantity;
            cart.TotalPrice = cart.UnitPrice * cart.Quantity;
            cart.Orderdate = DateTime.Now;

            return cart;
        }

        internal void AddOrder(InvoiceModel model, string totalBill, List<CartModel> list)
        {
            if (list != null)
            {
                var medList = new List<OrderItem>();
                string quantityListAsString = null;
                foreach (var item in list)
                {
                    var medicine = _orderService.GetMedicine(item.MedicineId);
                    medList.Add(new OrderItem
                    {
                        MedicineId = medicine.Id
                    }) ;
                    quantityListAsString += item.Quantity + " ";
                }
                _orderService.AddAnOrder(new Order
                {
                    UserId = model.UserId,
                    DelivaryAdress = model.Adress,
                    ContactNo = model.ContactNo,
                    PaymentType = model.PaymentType,
                    DeliveryStatus = "Pending",
                    OrderedMedicines = medList,
                    QuantitiesListAsString = quantityListAsString,
                    Orderdate = DateTime.Now,
                    TotalPrice = Convert.ToDouble(totalBill)
                }); ;

            }
        }
    }
}
