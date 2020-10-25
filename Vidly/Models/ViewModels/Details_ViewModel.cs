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

        public List<Movie> Movies { get; set; }

        public List<Customer> Customers { get; set; }

    }
}
