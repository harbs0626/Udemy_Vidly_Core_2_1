using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Models;
using Vidly.Models.ViewModels;

namespace Vidly.Controllers
{
    public class RegisterController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private AppIdentityDbContext _identityContext;

        public RegisterController (UserManager<AppUser> _userMgr, SignInManager<AppUser> _signInMgr,
            AppIdentityDbContext _identityCtx)
        {
            this._userManager = _userMgr;
            this._signInManager = _signInMgr;
            this._identityContext = _identityCtx;
        }

        public ViewResult Register()
        {
            ViewBag.Title = "Registration";

            Form_User_ViewModel _model = new Form_User_ViewModel
            {
                AppUser = new AppUser(),
                AppRoles = this._identityContext.Roles
                    .Where(r => r.Name != "Admin").ToList()
        };

            return View(_model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(Form_User_ViewModel _model)
        {
            if (ModelState.IsValid)
            {
                _model.AppRoles = this._identityContext.Roles
                    .Where(r => r.Name != "Admin").ToList();

                string _userName = _model.AppUser.FirstName.Substring(0, 1) +
                    _model.AppUser.LastName.Replace(" ", string.Empty).ToLower();

                string _verificationCode = Guid.NewGuid().ToString()
                    .Replace("-", string.Empty).ToLower();

                AppUser _appUser = new AppUser
                {
                    UserName = _userName,
                    FirstName = _model.AppUser.FirstName,
                    LastName = _model.AppUser.LastName,
                    Address = _model.AppUser.Address,
                    ContactNumber = _model.AppUser.ContactNumber,
                    Email = _model.AppUser.EmailAddress,
                    EmailAddress = _model.AppUser.EmailAddress,
                    IsVerified = true,
                    VerificationCode = _verificationCode
                };

                var _getRole = _model.AppRoles
                    .SingleOrDefault(m => m.Id == _model.AppUser.UserType);
                _appUser.UserType = _getRole.Id;

                string _newPasswordHash = this._userManager.PasswordHasher
                    .HashPassword(_appUser, _model.AppUser.Password);
                _appUser.PasswordHash = _newPasswordHash;

                IdentityResult _result = await this._userManager
                    .CreateAsync(_appUser, _model.AppUser.Password);

                if (_result.Succeeded)
                {
                    // Assign role to registered user
                    if (!await this._userManager.IsInRoleAsync(_appUser, _getRole.Name))
                    {
                        await this._userManager.AddToRoleAsync(_appUser, _getRole.Name);
                    }
                }
                else
                {
                    foreach(IdentityError _error in _result.Errors)
                    {
                        ModelState.AddModelError(_error.Code, _error.Description);
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
