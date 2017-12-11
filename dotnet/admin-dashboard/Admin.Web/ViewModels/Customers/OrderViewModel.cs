using System;

namespace Admin.Web.ViewModels.Customers
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
    }
}