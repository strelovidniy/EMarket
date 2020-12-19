using System.Linq;
using EMarket.Models;
using Microsoft.AspNetCore.Mvc;

namespace EMarket.Controllers
{
    public class ProductController:Controller
    {
        public IActionResult Index(int id)
        {
            Product product;

            using (var db = new AppContext())
            {
                product = db.Products.FirstOrDefault(p => p.Id == id);
            }

            return View(product);
        }
    }
}