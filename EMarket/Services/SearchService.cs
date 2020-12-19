using System.Collections.Generic;
using System.Linq;
using EMarket.Models;

namespace EMarket.Services
{
    public class SearchService : ISearchService
    {
        public List<Product> GetAllProductsByName(string query)
        {
            using (var db = new AppContext())
            {
                return db.Products.Where(pr => pr.Name.Contains(query)).ToList();
            }
        }

        public Product GetProductById(int id)
        {
            using (var db = new AppContext())
            {
                return db.Products.SingleOrDefault(pr => pr.Id == id);
            }
        }

        public List<Product> GetAll()
        {
            using (var db = new AppContext())
            {
                return db.Products.ToList();
            }
        }
    }
}