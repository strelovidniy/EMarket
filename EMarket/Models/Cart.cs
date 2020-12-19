﻿using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using EMarket.Models;

namespace Store.Web.Models
{
    public class Cart
    {
        public IDictionary<int, int> Items { get; set; }= new Dictionary<int, int>();
        public decimal Amount { get; set; }
    }
}