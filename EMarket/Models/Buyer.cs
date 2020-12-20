using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EMarket.Models
{
    public class Buyer
    {
        public int Id { get; set; }

        [Required] [MinLength(3)] [MaxLength(50)]
        public string FirstName { get; set; }

        [Required] [MinLength(3)] [MaxLength(50)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MinLength(8)] [MaxLength(16)]
        public string Password { get; set; }

        public List<Order> Orders { get; set; }
    }
}
