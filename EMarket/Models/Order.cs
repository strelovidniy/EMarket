﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMarket.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Delivery Delivery { get; set; }
        public int DeliveryId { get; set; }
        public int DestinationId { get; set; }
        public List<ProductOrder> ProductOrder { get; set; } = new List<ProductOrder>();
    }
}
