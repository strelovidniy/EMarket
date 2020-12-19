using System.Linq;
using EMarket.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace EMarket.Controllers
{
    public class CartController:Controller
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
            
            if(product!=null)
                cart.Amount += product.Price;

            HttpContext.Session.Set(cart);

            return RedirectToAction("Index", "Product", new {id});
        }

        
    }
}