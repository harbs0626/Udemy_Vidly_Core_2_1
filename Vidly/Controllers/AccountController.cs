using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vidly.Models;
using Vidly.Models.ViewModels;

namespace Vidly.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;

        public AccountController (UserManager<AppUser> _userMgr, SignInManager<AppUser> _signInMgr)
        {
            this._userManager = _userMgr;
            this._signInManager = _signInMgr;
        }

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

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(Form_Login_ViewModel _model)
        {
            string _errorMessage = string.Empty;

            if (ModelState.IsValid)
            {
                AppUser _user = await this._userManager.FindByNameAsync(_model.UserName);
                if (_user != null)
                {
                    HttpContext.Session.SetString("auth", "completed");
                    if (_user.UserName.ToLower() == "admin")
                    {
                        if ((await this._signInManager.PasswordSignInAsync(
                            _user, _model.Password, false, false)).Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            _errorMessage = "Invalid Username or Password";
                        }
                    }
                    else
                    {
                        if (_user.IsVerified == false)
                        {
                            _errorMessage = "User is not yet verified. Please contact Administrator.";
                        }
                        else
                        {
                            if ((await this._signInManager.PasswordSignInAsync(_user,
                                _model.Password, false, false)).Succeeded)
                            {
                                return Redirect(_model.ReturnUrl ?? "/");
                            }
                            else
                            {
                                _errorMessage = "Invalid Username or Password";
                            }
                        }
                    }
                }
                else
                {
                    _errorMessage = "Invalid Username or Password";
                }
                ModelState.AddModelError("", _errorMessage);
            }
            return View(_model);
        }

        public async Task<RedirectResult> Logout(Form_Login_ViewModel _model)
        {
            await this._signInManager.SignOutAsync();
            return Redirect(_model.ReturnUrl ?? "/");
        }

    }
}
