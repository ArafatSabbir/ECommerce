using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NecessaryDrugs.Web.Areas.Admin.Models
{
    public class NotificationModel
    {
        public string TypeCssClass { get; private set; }
        public string SignCssClass { get; private set; }
        public string Message { get; set; }
        public string HeaderText { get; set; }
        public NotificationModel(string headerText, string message, Notificationtype type)
        {
            HeaderText = headerText;
            Message = message;
            TypeCssClass = type == Notificationtype.Success ? "success" : "danger";
            SignCssClass = type == Notificationtype.Success ? "check" : "ban";
        }
    }
    public enum Notificationtype {
        Success,
        Fail
    }
}
