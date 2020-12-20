using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using AppContext = EMarket.Models.AppContext;

namespace EMarket.Controllers
{
    public class OrderController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            HttpContext.Session.TryGetCart(out Cart cart);
            if (cart == null || !cart.Items.Any())
                return RedirectToAction("Index", "Home");
            Dictionary<Product, int> products = new Dictionary<Product, int>();
            await using AppContext db = new AppContext();
            foreach (var entry in cart.Items)
            {
                products.Add(db.Products.Include(p => p.Seller)
                    .FirstOrDefaultAsync(p => p.Id == entry.Key).Result, entry.Value);
            }
            ViewBag.Products = products;
            ViewBag.Deliveries = db.Deliveries.ToList();
            return View(new Order());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            return View();
        }
    }
}
