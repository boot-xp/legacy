﻿namespace Admin.Web.ViewModels.Customers
{
    public class CreateViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string AddressLine1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string Country { get; set; }
    }
}