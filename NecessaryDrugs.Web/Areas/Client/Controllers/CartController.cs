using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NecessaryDrugs.Core.Entities;
using NecessaryDrugs.Web.Areas.Client.Models;
using Newtonsoft.Json;

namespace NecessaryDrugs.Web.Areas.Client.Controllers
{
    [Area("Client")]
    public class CartController : Controller
    {
        private readonly UserManager<NormalUser> _userManager;
        public CartController(UserManager<NormalUser> userManager)
        {
            _userManager = userManager;
        }
        List<CartModel> list = new List<CartModel>();
        public IActionResult AddToCart(int id)
        {
            var model = new MedicineViewModel();
            model.GetDetails(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult AddToCart(int id, int quantity)
        {
            var model = new CartModel();
            var cart = model.AddToCart(id, quantity);
            List<CartModel> list2 = null;
            if (TempData["cart"] == null)
            {
                list.Add(cart);
                TempData["cart"] = JsonConvert.SerializeObject(list);
                list2 = JsonConvert.DeserializeObject<List<CartModel>>(TempData["cart"] as string);
                //list2 = ViewData["cart"] as List<CartModel>;
            }
            else
            {
                list2 = JsonConvert.DeserializeObject<List<CartModel>>(TempData["cart"] as string);
                //list2 = ViewData["cart"] as List<CartModel>;
                int flag = 0;
                foreach (var item in list2)
                {
                    if (item.MedicineId == cart.MedicineId)
                    {
                        item.Quantity += cart.Quantity;
                        item.TotalPrice += cart.TotalPrice;
                        flag = 1;
                    }

                }
                if (flag == 0)
                {
                    list2.Add(cart);
                    //new item
                }
                TempData["cart"] = JsonConvert.SerializeObject(list2);
            }
            TempData["item_count"] = JsonConvert.SerializeObject(list2.Count);
            TempData.Keep();
            return RedirectToAction("Index","Medicine");
        }
        public IActionResult DelTempData()
        {
            TempData.Remove("cart");
            TempData.Remove("item_count");
            return RedirectToAction("Index", "Medicine");
        }

        public ActionResult Cart()
        {
            List<CartModel> list2 = null;
            if (TempData["cart"] != null)
            {
                double x = 0;
                list2 = JsonConvert.DeserializeObject<List<CartModel>>(TempData["cart"] as string);
                //list2 = ViewData["cart"] as List<CartModel>;
                foreach (var item in list2)
                {
                    x += item.TotalPrice;
                }
                if (list2?.Count() == 0)
                {
                    TempData.Remove("total");
                    TempData.Remove("item_count");
                }
                else
                {
                    TempData["total"] = JsonConvert.SerializeObject(x);
                    TempData["item_count"] = JsonConvert.SerializeObject(list2.Count);
                }
                 
            }
            TempData.Keep();
            return View(list2);
        }
        public ActionResult Remove(int? id)
        {
            List<CartModel> list2 = null;
            if (TempData["cart"] == null)
            {
                TempData.Remove("total");
                TempData.Remove("cart");
            }
            else
            {
                list2 = JsonConvert.DeserializeObject<List<CartModel>>(TempData["cart"] as string);
                //list2 = ViewData["cart"] as List<CartModel>;
                int index = 0;
                foreach(var item in list2)
                {
                    if (item.MedicineId == id)
                    {
                        index = list2.IndexOf(item);
                        break;
                    }
                }
                list2.RemoveAt(index);
                if (list2?.Count() == 0)
                {
                    TempData.Remove("cart");
                    TempData.Remove("item_count");
                }
                else
                {
                    TempData["cart"] = JsonConvert.SerializeObject(list2);
                }
            }
            TempData.Keep();

            return RedirectToAction("Cart");
        }
        [Authorize]
        public async Task<ActionResult> Checkout()
        {
            var list2 = JsonConvert.DeserializeObject<List<CartModel>>(TempData["cart"] as string);
            //var list2 = ViewData["cart"] as List<CartModel>;
            var model = new InvoiceModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            TempData["user"] = JsonConvert.SerializeObject(user);
            return View(model.GenerateInvoice(user, list2));
        }

        [HttpPost]
        public ActionResult Checkout(InvoiceModel invoiceModel)
        {
            var list2 = JsonConvert.DeserializeObject<List<CartModel>>(TempData["cart"] as string);
            //var list2 = ViewData["cart"] as List<CartModel>;
            var totalBill = JsonConvert.DeserializeObject<string>(TempData["total"] as string);
            //var totalBill = ViewData["total"] as string;
            var cartModel = new CartModel();
            cartModel.AddOrder(invoiceModel,totalBill,list2);
            TempData.Remove("total");
            TempData.Remove("cart");
            TempData.Remove("item_count");
            return RedirectToAction("OrderSucceeded");
        }
        public IActionResult OrderSucceeded()
        {
            return View();
        }
    }
}
