using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vidly.Models
{
    public static class SeedData_Identity
    {
        private const string _adminUser = "admin";
        private const string _adminPassword = "Password0!";
        private const string _adminRoleName = "Admin";

        private const string _managerUser = "manager";
        private const string _managerPassword = "Password9!";
        private const string _managerRoleName = "Manager";

        private const string _employeeUser = "harbin";
        private const string _employeePassword = "Password8!";
        private const string _employeeRoleName = "Employee";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            RoleManager<IdentityRole> _roleManager = app.ApplicationServices
                .GetRequiredService<RoleManager<IdentityRole>>();

            // Create Multiple Roles
            string[] _roles = { _adminRoleName, _managerRoleName, _employeeRoleName };

            foreach(string r in _roles)
            {
                IdentityRole _role = await _roleManager.FindByNameAsync(r);
                if (_role == null)
                {
                    _role = new IdentityRole(r);
                    await _roleManager.CreateAsync(_role);
                }
            }

            UserManager<AppUser> _userManager = app.ApplicationServices
                .GetRequiredService<UserManager<AppUser>>();

            CreateAdminUser(app, _userManager);
            CreateOtherUsers(app, _userManager);
        }

        static async void CreateAdminUser(IApplicationBuilder app, UserManager<AppUser> _userManager)
        {
            // Create Admin User
            AppUser _appUser = await _userManager.FindByNameAsync(_adminUser);
            if (_appUser == null)
            {
                _appUser = new AppUser
                {
                    UserName = _adminUser,
                    IsVerified = true
                };

                await _userManager.CreateAsync(_appUser, _adminPassword);
                await _userManager.AddToRoleAsync(_appUser, _adminRoleName);
            }
            else
            {
                if (!await _userManager.IsInRoleAsync(_appUser, _adminRoleName))
                {
                    await _userManager.AddToRoleAsync(_appUser, _adminRoleName);
                }
            }
        }

        static async void CreateOtherUsers(IApplicationBuilder app, UserManager<AppUser> _userManager)
        {
            // Create Other Users
            string[,] _otherUsers = new string[2, 3] 
            { 
                { _managerUser, _managerPassword, _managerRoleName }, 
                { _employeeUser, _employeePassword, _employeeRoleName } 
            };

            for (int o = 0; o <= _otherUsers.GetUpperBound(0); o++)
            {
                AppUser _appUser = await _userManager.FindByNameAsync(_otherUsers[o, 0]);
                if (_appUser == null)
                {
                    _appUser = new AppUser
                    {
                        UserName = _otherUsers[o, 0],
                        IsVerified = true
                    };

                    await _userManager.CreateAsync(_appUser, _otherUsers[o, 1]);
                    await _userManager.AddToRoleAsync(_appUser, _otherUsers[o, 2]);
                }
                else
                {
                    if (!await _userManager.IsInRoleAsync(_appUser, _otherUsers[o, 2]))
                    {
                        await _userManager.AddToRoleAsync(_appUser, _otherUsers[o, 2]);
                    }
                }
            }
        }

    }
}
