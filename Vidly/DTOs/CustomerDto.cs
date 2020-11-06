using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Models;

namespace Vidly.DTOs
{
    public class CustomerDto
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public DateTime? BirthDate { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        public int? MembershipTypeId { get; set; }

        public MembershipTypeDto MembershipType { get; set; }
        
        // Harbs - 2020-11-06
        // Upon observation, if "MembershipTypeDto" as a variable name
        // it returns null. But if I use "MembershipType", it returns the
        // expected values.

    }
}
