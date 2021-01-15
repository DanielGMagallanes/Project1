using BusinessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_1.Controllers
{
    public class StoreController : Controller
    {
        private StoreLevelPrograms _storeLevelPrograms;
        private readonly ILogger<StoreController> _logger;
        public StoreController(StoreLevelPrograms storeLevelPrograms, ILogger<StoreController> logger)
        {
            _storeLevelPrograms = storeLevelPrograms;
            _logger = logger;
        }

        // GET: StoreController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetStoreInvertory(int id)
        {
            List<StoreViewModel> storViewModels = new List<StoreViewModel>();
            storViewModels = _storeLevelPrograms.ProductSelection(id);
            return View("StoreInvertory2", storViewModels);
        }


        // GET: StoreController/Create
        [Authorize]
        public ActionResult CreateProduct()
        {
            return View();
        }

        // POST: StoreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(IFormCollection collection)
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
        [Authorize]
        // GET: StoreController/Edit/5
        public ActionResult EditProduct(int id)
        {
            return View();
        }

        // POST: StoreController/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(int id, IFormCollection collection)
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

        // GET: StoreController/Delete/5
        [Authorize]
        public ActionResult DeleteProduct(int id)
        {
            return View();
        }

        // POST: StoreController/Delete/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(int id, IFormCollection collection)
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
