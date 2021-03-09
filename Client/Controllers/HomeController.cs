using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IBLL;
using IDAL;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserDal _iUserDal;
        private readonly IUserBLL _iUserBll;

        public HomeController(ILogger<HomeController> logger,
            IUserDal iUserDal,
            IUserBLL iUserBll)
        {
            _logger = logger;
            _iUserDal = iUserDal;
            _iUserBll = iUserBll;
        }

        public IActionResult Index()
        {
            var model =  this._iUserBll.Login("123");
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
