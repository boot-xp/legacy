using System;
using Admin.Web.Models;

namespace Admin.Web.ViewModels.Orders
{
    public class CreateViewModel
    {
        public DateTime OrderDate { get; set; }
        public Customer[] Customers { get; set; }
    }
}