using System.ComponentModel.DataAnnotations;

namespace EMarket.Models
{
    public class Delivery
    {
        public int Id { get; set; }

        [MinLength(3)] [MaxLength(50)]
        public string Name { get; set; }

        public int Duration { get; set; }

        public decimal Price { get; set; }
    }
}
