using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AppContext = EMarket.Models.AppContext;

namespace EMarket.Controllers
{
    //[Authorize(Roles = "Seller")]
    public class SellerController : Controller
    {
        public async Task<IActionResult> Index()
        {
            await using AppContext db = new AppContext();
            var orders = db.Orders.Include(s=>s.Seller)
                .Where(o => o.Seller.Email == User.FindFirst(u 
                    => u.Type == ClaimTypes.Email).Value)
                .Include(o => o.Buyer)
                .Include(o => o.Delivery)
                .Include(o => o.ProductOrder)
                .ThenInclude(po => po.Product).ToList();
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            await using AppContext db = new AppContext();
            var categories = db.Categories.ToList();
            ViewBag.Categories = categories;
            
            return View(new Product());
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                await using AppContext db = new AppContext();
                product.SellerId = Int32.Parse(User.FindFirst("Id")?.Value ?? string.Empty);
                await db.Products.AddAsync(product);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
