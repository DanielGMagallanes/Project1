using BusinessLayer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer;
using ModelLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace project_1.Controllers
{
    public class CustomerController : Controller
    {
        private StoreLevelPrograms _storeLevelPrograms1;
        private MapperClass _mapper;
        const string SessionID = "Guid";
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(StoreLevelPrograms storeLevelPrograms, MapperClass map, ILogger<CustomerController> logger)
        {
            _storeLevelPrograms1 = storeLevelPrograms;
            _mapper = map;
            _logger = logger;
        }
        
        [HttpGet]
        public ActionResult CustomerLogin()
        {
            return View();
        }
        [HttpPost]
        [ActionName("CustomerLogin")]
        public async Task<IActionResult> CustomerLogin(CustomerViewModel customerViewModel)
        {
           
            var identity = _mapper.Authenticate(customerViewModel.firstName, customerViewModel.lastName);
            if(identity == null)
            {
                return RedirectToAction(nameof(CustomerLogin));
            }

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties());
            Customer cus = _mapper.GetCustomer(customerViewModel.firstName, customerViewModel.lastName);
            HttpContext.Session.SetString(SessionID,cus.Customer_Id.ToString());

            return RedirectToAction("MainPage","Home");
        }
        [ActionName("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Guid");
            return RedirectToAction("Index","Home");
        }



        // GET: CustomerController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
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

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerController/Edit/5
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

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5
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
