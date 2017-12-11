using System.Collections.Generic;

namespace Admin.Web.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public ICollection<OrderLineItem> OrderLineItems { get; set; }
    }
}