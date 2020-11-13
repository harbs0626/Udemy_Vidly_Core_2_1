using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vidly.Models;
using Vidly.Models.ViewModels;

namespace Vidly.Controllers
{
    public class AdminController : Controller
    {
        private Form_User_ViewModel _users;
        private AppIdentityDbContext _context;

        public AdminController(AppIdentityDbContext _ctx)
        {
            this._context = _ctx;
        }

        protected override void Dispose(bool disposing)
        {
            this._context.Dispose();
        }

        public ViewResult UserList()
        {
            ViewBag.Title = "List of Users";

            this._users = new Form_User_ViewModel();
            this._users.AppUsers = this._context.AppUsers
                .Where(u => u.UserType != null)
                .OrderBy(u => u.Id);
            this._users.AppRoles = this._context.Roles
                .Where(r => r.Name != "Admin").ToList();

            return View(this._users);
        }

    }
}
