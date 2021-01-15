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
    }
}
