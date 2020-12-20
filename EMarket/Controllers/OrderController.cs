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
    //todo
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

            foreach (var product in products)
            {
                if (product.Key.Count < product.Value)
                    return View();
            }

            ViewBag.Products = products;
            ViewBag.Deliveries = db.Deliveries.ToList();
            ViewBag.TotalPrice = cart.TotalPrice;
            return View(new Order());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            HttpContext.Session.TryGetCart(out Cart cart);
            if (cart == null || !cart.Items.Any())
                return RedirectToAction("Index", "Home");
            await using AppContext db = new AppContext();
            var allProducts = db.Products.Where(p => cart.Items.Keys.Contains(p.Id) && p.Count >= cart.Items[p.Id]);
            var orders = allProducts.GroupBy(key => key.SellerId).Select(p => new
            {
                SellerId = p.Key,
                Products = p.Select(pr => pr)
            });
            foreach (var item in orders)
            {
                Order newOrder = (await db.Orders.AddAsync(new Order()
                {
                    BuyerId = order.BuyerId,
                    DeliveryId = order.DeliveryId,
                    Destination = order.Destination,
                    SellerId = item.SellerId
                })).Entity;

                foreach (var product in item.Products)
                {
                    product.Count -= cart.Items[product.Id];
                    order.ProductOrder.Add(new ProductOrder()
                    {
                        Count = cart.Items[product.Id],
                        Price = product.Price,
                        ProductId = product.Id,
                        OrderId = newOrder.Id
                    });
                    await db.ProductOrders.AddAsync(new ProductOrder()
                    {
                        Count = cart.Items[product.Id],
                        Price = product.Price,
                        ProductId = product.Id,
                        OrderId = newOrder.Id
                    });

                }
                await db.SaveChangesAsync();
            }

            return View();
        }
    }
}
