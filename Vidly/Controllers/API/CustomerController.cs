using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext ctx)
        {
            this._context = ctx;
        }

        // GET: API/<controller>
        [HttpGet]
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return this._context.Customers.ToList()
                .Select(Mapper.Map<Customer, CustomerDto>)
                .OrderBy(c => c.Id);
        }

        // GET: API/<controller>/<id>
        [HttpGet("{Id}")]
        public IActionResult GetCustomer(int Id)
        {
            var _customer = this._context.Customers
                .SingleOrDefault(c => c.Id == Id);

            if (_customer == null)
            {
                return NotFound("Customer does not exist!");
            }

            return Ok(Mapper.Map<Customer, CustomerDto>(_customer));
        }

        // POST: API/<controller>
        [HttpPost]
        public IActionResult CreateCustomer(CustomerDto _customerDto)
        {
            if (ModelState.IsValid)
            {
                var _customer = Mapper.Map<CustomerDto, Customer>(_customerDto);
                this._context.Customers.Add(_customer);
                this._context.SaveChanges();

                _customerDto.Id = _customer.Id;
            }

            return Ok("Successfully added Customer.");
        }

        // PUT: API/<controller>/<id>
        [HttpPut("{Id}")]
        public IActionResult UpdateCustomer(int Id, CustomerDto _customerDto)
        {
            var _customerInDb = this._context.Customers
                .SingleOrDefault(c => c.Id == Id);

            if (_customerInDb == null)
            {
                return NotFound("Customer does not exist!");
            }
            else
            {
                //Mapper.Map(_customerInDb, _customerDto);
                //Mapper.Map(_customerDto, _customerInDb);

                var _customer = Mapper.Map<CustomerDto, Customer>(_customerDto);

                _customerInDb.Name = _customer.Name;
                _customerInDb.BirthDate = _customer.BirthDate;
                _customerInDb.MembershipTypeId = _customer.MembershipTypeId;
                _customerInDb.IsSubscribedToNewsLetter = _customer.IsSubscribedToNewsLetter;

                this._context.SaveChanges();
            }

            return Ok("Successfully updated Customer.");
        }

        // DELETE: API/<controller>/<id>
        [HttpDelete("{Id}")]
        public IActionResult DeleteCustomer(int Id)
        {
            var _customerInDb = this._context.Customers
                .SingleOrDefault(c => c.Id == Id);

            if (_customerInDb == null)
            {
                return NotFound("Customer does not exist!");
            }
            else
            {
                this._context.Customers.Remove(_customerInDb);
                this._context.SaveChanges();
            }

            return Ok("Successfully deleted Customer.");
        }

    }
}
