using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NecessaryDrugs.Core.Entities;
using NecessaryDrugs.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NecessaryDrugs.Web.Areas.Client.Models
{
    public class InvoiceModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ContactNo { get; set; }
        public List<CartModel> MedList { get; set; }
        public string Adress { get; set; }
        public int ItemBought { get; set; }
        public double TotalBill { get; set; }
        public string PaymentType { get; set; }
        internal InvoiceModel GenerateInvoice(NormalUser user, List<CartModel> list)
        {
            return new InvoiceModel
            {
                UserId=user.Id,
                UserName = user.FirstName + user.LastName,
                ContactNo = user.PhoneNumber,
                Adress = user.Adress,
                MedList=list
            };
        }
    }
}
