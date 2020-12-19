﻿using EMarket.Models;
 using Microsoft.AspNetCore.Http;

namespace Store.Web.Models
{
    public class OrderModel
    { 
        public int Id { get; set; }
        public  Product[] Items { get; set; }= new Product[0];
        public  int TotalCount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}