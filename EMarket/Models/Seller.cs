using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EMarket.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string City { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MinLength(8)][MaxLength(16)]
        public string Password { get; set; }
        public List<Product> Products { get; set; }
        public List<Order> Orders { get; set; }
    }
}
