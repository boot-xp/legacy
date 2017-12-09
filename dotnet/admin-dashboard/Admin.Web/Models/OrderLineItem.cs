﻿namespace Admin.Web.Models
{
    public class OrderLineItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public Order Order { get; set; }
    }
}