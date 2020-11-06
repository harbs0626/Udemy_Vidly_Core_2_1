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

        public double SignUpFee { get; set; }

        public int DurationInMonths { get; set; }

        public double DiscountRate { get; set; }

        public static readonly int? Unknown = null;
        public static readonly int PayAsYouGo = 1;
        public static readonly int Monthly = 2;
        public static readonly int Quarterly = 3;
        public static readonly int Annual = 4;

    }
}
