using System.Collections.Generic;
using EMarket.Models;

namespace EMarket.Services
{
    public interface ISearchService
    {
        List<Product> GetAllProductsByName(string query);
        Product GetProductById(int id);
        List<Product> GetAll();
    }
}