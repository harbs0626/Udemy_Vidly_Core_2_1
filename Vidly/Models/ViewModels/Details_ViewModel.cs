using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vidly.Models.ViewModels
{
    public class Details_ViewModel
    {
        public Customer Customer { get; set; }

        public Movie Movie { get; set; }

        public IEnumerable<Movie> Movies { get; set; }

        public IEnumerable<Customer> Customers { get; set; }

    }
}
