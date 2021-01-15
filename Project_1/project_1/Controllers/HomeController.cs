using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer;
using project_1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace project_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private StoreLevelPrograms _storeLevelPrograms;
        public HomeController(StoreLevelPrograms storeLevelPrograms, ILogger<HomeController> logger)
        {
            _storeLevelPrograms = storeLevelPrograms;
            _logger = logger;
        }

    
        public IActionResult MainPage()
        {
            List<Store> list = new List<Store>();
            list = _storeLevelPrograms.GetStores();

            return View("MainPageStoreViewModel",list);
        }

        public IActionResult Index()
        {
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
