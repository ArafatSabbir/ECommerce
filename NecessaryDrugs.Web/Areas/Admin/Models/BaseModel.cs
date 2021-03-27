using NecessaryDrugs.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NecessaryDrugs.Web.Areas.Admin.Models
{
    public abstract class BaseModel
    {
        public NotificationModel Notification { get; set; }
    }
}
