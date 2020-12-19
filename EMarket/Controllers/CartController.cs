using System.Collections.Generic;
using System.Linq;
using EMarket.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace EMarket.Controllers
{
    public class CartController : Controller
    {
        public IActionResult AddItem(int id)
        {
            Cart cart;
            Product product;
            using (var db = new AppContext())
            {
                product = db.Products.FirstOrDefault(p => p.Id == id);
            }
            if (!HttpContext.Session.TryGetCart(out cart))
            {
                cart = new Cart();
            }

            if (cart.Items.ContainsKey(id))
            {
                cart.Items[id]++;
            }
            else
            {
                cart.Items[id] = 1;
            }

            if (product != null)
                cart.TotalPrice += product.Price;

            HttpContext.Session.Set(cart);

            return RedirectToAction("Index", "Product", new { id });
        }

        public IActionResult Index()
        {
            Cart cart;
            List<Product> products;
            if (HttpContext.Session.TryGetCart(out cart))
            {
                using (var db = new AppContext())
                {
                    var productIds = cart.Items.Select(item => item.Key).ToList();
                    products = db.Products.Where(p => productIds.Contains(p.Id)).ToList();
                    return View(products);
                }
            }

            return View("Empty");
        }

    }
}