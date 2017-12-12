namespace Admin.Web.ViewModels.Orders
{
    public class ApiCreateViewModel
    {
        public int CustomerId { get; set; }
        
        public ApiOrderLineItemViewModel[] Items { get; set; }
    }
}