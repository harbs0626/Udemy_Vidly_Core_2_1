using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vidly.Models.ViewModels;

namespace Vidly.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            ViewBag.Title = "Login";

            Form_Login_ViewModel _model = new Form_Login_ViewModel 
            { 
                ReturnUrl = returnUrl 
            };

            return View();
        }
    }
}
