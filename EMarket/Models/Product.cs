using System.Collections.Generic;

namespace EMarket.Models
{
    public enum Category
    {
        Milk, Meat, Fruits, Vegetables, Animals
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Category Category { get; set; }

        public decimal Price { get; set; }

        public int SellerId { get; set; }
        public Seller Seller { get; set; }

        public decimal Quantity { get; set; }
        public int Count { get; set; }

        public List<ProductOrder> ProductOrder { get; set; } = new List<ProductOrder>();
    }
}