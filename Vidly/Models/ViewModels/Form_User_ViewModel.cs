using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vidly.Models.ViewModels
{
    public class Form_User_ViewModel
    {
        public AppUser AppUser { get; set; }

        public List<IdentityRole> AppRoles { get; set; }

        public IEnumerable<AppUser> AppUsers { get; set; }

    }
}
