using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vidly.Models.ViewModels
{
    public class Form_Customer_ViewModel
    {
        public Customer Customer { get; set; }

        public IEnumerable<MembershipType> MembershipTypes { get; set; }

    }
}
