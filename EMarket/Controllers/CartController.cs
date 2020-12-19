using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMarket.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace EMarket.Controllers
{
    public class CartController : Controller
    {
        public async Task<IActionResult> AddItem(int id, int count = 1)
        {
            Product product;
            await using (var db = new AppContext())
            {
                product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
            }
            if (!HttpContext.Session.TryGetCart(out Cart cart))
            {
                cart = new Cart();
            }

            if (cart.Items.ContainsKey(id))
                cart.Items[id] += count;
            else
                cart.Items[id] = count;

            if (product != null)
                cart.TotalPrice += product.Price;

            HttpContext.Session.Set(cart);
            return RedirectToAction("Index", "Product", new { id });
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.TryGetCart(out Cart cart))
            {
                await using (var db = new AppContext())
                {
                    Dictionary<Product, int> products = new Dictionary<Product, int>();
                    foreach (var entry in cart.Items)
                    {
                        products.Add(db.Products.Include(p => p.Seller)
                            .FirstOrDefaultAsync(p => p.Id == entry.Key).Result, entry.Value);
                    }
                    return View(products);
                }
            }

            return View("Empty");
        }

    }
}