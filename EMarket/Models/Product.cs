using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMarket.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category  Category { get; set; }
        public decimal Price { get; set; }

        public List<ProductOrder> ProductOrder { get; set; } = new List<ProductOrder>();
    }
}
