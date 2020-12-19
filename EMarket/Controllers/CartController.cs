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
            //TODO: add get by id from database 
            var product = new Product();
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

            cart.Amount += product.Price;

            HttpContext.Session.Set(cart);

            return RedirectToAction("Index", "Product", new {id});
        }

        
    }
}