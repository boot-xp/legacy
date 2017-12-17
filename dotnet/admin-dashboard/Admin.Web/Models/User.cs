using Microsoft.AspNetCore.Identity;

namespace Admin.Web.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public bool IsAdmin { get; set; }
    }
}