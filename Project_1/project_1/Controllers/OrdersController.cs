using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer;
using ModelLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_1.Controllers
{
    public class OrdersController : Controller
    {
        private StoreLevelPrograms _storeLevelPrograms;
        private MapperClass _mapper;
        private readonly ILogger<OrdersController> _logger;
        public OrdersController(StoreLevelPrograms storeLevelPrograms, MapperClass map, ILogger<OrdersController> logger)
        {
            _storeLevelPrograms = storeLevelPrograms;
            _mapper = map;
            _logger = logger;
        }


        /// <summary>
        /// This will show everything in the order. how many items, the price, total price, and 
        /// final total.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult FullOrderDetails(int id)
        {
            double total = 0;
            List<FullOrderViewModel> fullorder = new List<FullOrderViewModel>();
            fullorder = _storeLevelPrograms.GetFullOrderDetails(id);
            foreach(var num in fullorder)
            {
                total += (num.pricePaid * num.qtyOrdered);
            }
            ViewBag.total = total;
            return View("FullOrderViewModel", fullorder);
        }

        /// <summary>
        /// This will just get all the past orders of a customer and list them
        /// </summary>
        /// <returns></returns>
        public ActionResult AllPastOrders()
        {
            List<OrderViewModel> orderViewModels = new List<OrderViewModel>();
            List<Orders> orders = new List<Orders>();
            orders =  _storeLevelPrograms.AllPastOrders(HttpContext.Session.GetString("Guid"));
            foreach(var entry in orders)
            {
                orderViewModels.Add(_mapper.ConvertOrdersToOrderViewModel(entry));
            }

            return View(orderViewModels);
        }

        /// <summary>
        /// This will just get all the past orders of a store and list them
        /// </summary>
        /// <returns></returns>
        public ActionResult AllPastStoreOrders(int idStore)
        {
            List<OrderViewModel> orderViewModels = new List<OrderViewModel>();
            List<Orders> orders = new List<Orders>();
            orders = _storeLevelPrograms.AllPastStoreOrders(idStore);
            foreach (var entry in orders)
            {
                orderViewModels.Add(_mapper.ConvertOrdersToOrderViewModel(entry));
            }

            return View(orderViewModels);
        }
    }
}
