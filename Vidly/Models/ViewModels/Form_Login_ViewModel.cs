using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vidly.Models.ViewModels
{
    public class Form_Login_ViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

    }
}
