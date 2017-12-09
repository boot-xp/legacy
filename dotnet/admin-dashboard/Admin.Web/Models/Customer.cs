using System.Collections.Generic;

namespace Admin.Web.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public Address Address { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}