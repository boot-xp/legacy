using Admin.Web.Models;

namespace Admin.Web.ViewModels.Users
{
    public class EditViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
    }
}