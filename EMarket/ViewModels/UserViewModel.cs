using System.Collections.Generic;
using EMarket.Models;

namespace EMarket.ViewModels
{
    public class UserViewModel
    {
        public List<Buyer> Buyers { get; set; }
        public List<Seller> Sellers { get; set; }
    }
}