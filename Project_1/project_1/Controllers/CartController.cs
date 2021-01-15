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
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private StoreLevelPrograms _storeLevelPrograms;
        public CartController(StoreLevelPrograms storeLevelPrograms, ILogger<CartController> logger)
        {
            _storeLevelPrograms = storeLevelPrograms;
            _logger = logger;
        }
        // GET: CartController
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Based on the store picked the cart will be filled with what the user as selected at 
        /// said store. Total calualtions will be made so you know how much it is going to be
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ViewCart()
        {
            List<Cart> carts = new List<Cart>();
            List<Item> items = new List<Item>();
            carts = _storeLevelPrograms.GetCartItems(HttpContext.Session.GetString("Guid"));
            double total = 0;
            total = _storeLevelPrograms.CheckOutTotal(carts);
            ViewBag.total = total;
            return View(carts);
        }

        /// <summary>
        /// Based on the store picked the cart will be filled with what the user as selected at 
        /// said store. Total calualtions will be made so you know how much it is going to be
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CheckOutCart(int id)
        {
            List<Cart> carts = new List<Cart>();
            carts = _storeLevelPrograms.GetCartItems(HttpContext.Session.GetString("Guid"));
            _storeLevelPrograms.CheckOutCounter(carts,id,HttpContext.Session.GetString("Guid"));
            return RedirectToAction("MainPage", "Home");
        }

        public ActionResult AddToCart(StoreViewModel svm)
        {
            _storeLevelPrograms.AddToCart(svm, HttpContext);
            return RedirectToAction("GetStoreInvertory","Store", new { id = svm.ID_store });
        }

        // POST: CartController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CartController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
