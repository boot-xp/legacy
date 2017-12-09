using System;
using System.Collections.Generic;

namespace Admin.Web.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        
        public ICollection<OrderLineItem> LineItems { get; set; }
    }
}