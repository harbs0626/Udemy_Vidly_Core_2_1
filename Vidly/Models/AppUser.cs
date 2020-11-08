using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vidly.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string ContactNumber { get; set; }

        public string UserType { get; set; }

        public string Password { get; set; }

        public string EmailAddress { get; set; }

        public bool IsVerified { get; set; }

        public string VerificationCode { get; set; }

    }
}
