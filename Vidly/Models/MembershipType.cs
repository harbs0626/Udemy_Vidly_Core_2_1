using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vidly.Models
{
    public class MembershipType
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal SignUpFee { get; set; }

        public int DurationInMonths { get; set; }

        public decimal DiscountRate { get; set; }

    }
}
