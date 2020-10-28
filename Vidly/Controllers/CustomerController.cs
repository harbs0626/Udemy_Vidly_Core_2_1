using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Models;
using Vidly.Models.ViewModels;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        private Details_ViewModel _details;
        private ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext ctx)
        {
            this._context = ctx;
        }

        protected override void Dispose(bool disposing)
        {
            this._context.Dispose();
        }

        public ViewResult Index()
        {
            ViewBag.Title = "Customers";

            this._details = new Details_ViewModel();
            this._details.Customers = this._context.Customers
                .Include(c => c.MembershipType)
                .OrderBy(c => c.Id);

            return View(this._details);
        }

        public object NewRecord()
        {
            ViewBag.Title = "New Customer";

            var _viewModel = new Form_Customer_ViewModel()
            {
                Customer = new Customer(),
                MembershipTypes = this._context.MembershipTypes.ToList()
            };

            return _viewModel;
        }

        public ViewResult Record()
        {
            return View(this.NewRecord());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Details_ViewModel _customer)
        {
            if (ModelState.IsValid)
            {
                if (_customer.Customer.Id == 0)
                {
                    this._context.Customers.Add(_customer.Customer);
                }
                else
                {
                    var _customerInDb = this._context.Customers
                        .SingleOrDefault(c => c.Id == _customer.Customer.Id);

                    if (_customerInDb != null)
                    {
                        _customerInDb.Name = _customer.Customer.Name;
                        _customerInDb.BirthDate = _customer.Customer.BirthDate;
                        _customerInDb.MembershipTypeId = _customer.Customer.MembershipTypeId;
                        _customerInDb.IsSubscribedToNewsLetter = _customer.Customer.IsSubscribedToNewsLetter;
                    }
                }

                this._context.SaveChanges();
            }
            else
            {
                return View("Form_Customer", this.NewRecord());
            }

            return RedirectToAction("Index", "Customer");
        }

        public ViewResult Details(int Id)
        {
            this._details = new Details_ViewModel();
            this._details.Customers = this._context.Customers
                .Include(c => c.MembershipType)
                .Where(c => c.Id == Id);

            foreach(var getCustomerName in this._details.Customers)
            {
                ViewBag.Title = getCustomerName.Name;
            }

            return View(this._details);
        }

        public ActionResult Edit(int Id)
        {
            ViewBag.Title = "Edit Customer";

            var _viewModel = new Form_Customer_ViewModel()
            {
                Customer = this._context.Customers.SingleOrDefault(c => c.Id == Id),
                MembershipTypes = this._context.MembershipTypes.ToList()
            };

            return View("Form_Customer", _viewModel);
        }

        public ActionResult Delete(int Id)
        {
            var _customerInDb = this._context.Customers
                .SingleOrDefault(c => c.Id == Id);

            if (_customerInDb != null)
            {
                this._context.Customers.Remove(_customerInDb);
                this._context.SaveChanges();
            }

            return RedirectToAction("Index", "Customer");
        }

    }
}
