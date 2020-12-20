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
        [HttpGet]
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
                cart.TotalPrice += product.Price * count;

            HttpContext.Session.Set(cart);
            return RedirectToAction("View", "Product", new { id });
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

                    ViewBag.Cart = cart;
                    return View(products);
                }
            }

            return View("Empty");
        }

        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.TryGetCart(out Cart cart))
            {
                cart.Items.Remove(id);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Reduce(int id)
        {
            if (HttpContext.Session.TryGetCart(out Cart cart))
            {
                if(cart.Items[id] > 0)
                    cart.Items[id]--;
                else
                {
                    cart.Items.Remove(id);
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Add(int id)
        {
            if (HttpContext.Session.TryGetCart(out Cart cart))
            {
                cart.Items[id]++;
            }
            return RedirectToAction("Index");
        }
    }
}