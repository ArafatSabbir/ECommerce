using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NecessaryDrugs.Core.Services;
using NecessaryDrugs.Web.Models;

namespace NecessaryDrugs.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger,IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
            //var mailsender = new MailSender(_config);
            //mailsender.Send(new List<MailMessage>
            //{
            //    new MailMessage
            //    {
            //        Body="This is test",
            //        Receiver="armanabdullah101@gmail.com",
            //        Sender="abdullahfaruquearman@gmail.com",
            //        SenderName="Arman",
            //        Subject="Test email"
            //    }
            //});
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
