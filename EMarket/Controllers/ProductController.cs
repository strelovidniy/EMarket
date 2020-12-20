#nullable enable
#nullable enable
#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMarket.Controllers
{
    public class ProductController:Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(Category? category)
        {
            await using AppContext db = new AppContext();
            List<Product> products;
            if (category == null)
            {
                products = (await db.Products.ToListAsync());
            }
            else
            {
                products = await db.Products.Where(p => p.Category == category).ToListAsync();
            }
            return View(products);
        }
        public IActionResult View(int id)
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