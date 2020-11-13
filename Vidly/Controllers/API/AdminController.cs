using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        private AppIdentityDbContext _context;

        public AdminController(UserManager<AppUser> _userManagerRepo, 
            AppIdentityDbContext _appIdentityDbCtx)
        {
            this._userManager = _userManagerRepo;
            this._context = _appIdentityDbCtx;
        }

        // GET: API/<controller>
        [HttpGet]
        public IActionResult GetUsers()
        {
            var _getUsers = this._context.AppUsers
                //.Include(u => u.IdentityRole)
                .ToList()
                .OrderBy(u => u.Id);

            return Ok(_getUsers);
        }

        // DELETE: API/<controller>/<id>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            AppUser _deleteUser = await this._userManager
                .FindByIdAsync(Id.ToString());

            if (_deleteUser == null)
            {
                return NotFound("User does not exist!");
            }

            string _message = "Successfully deleted Customer.";
            IdentityResult _result = await this._userManager.DeleteAsync(_deleteUser);
            if (_result.Succeeded)
            {
                _message = "Successfully deleted Customer.";
            }
            return Ok(_message);
        }

    }
}
