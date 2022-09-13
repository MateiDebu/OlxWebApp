using Microsoft.AspNetCore.Identity;

namespace DataAccess.Database.Models
{
    public class ApplicationUser:IdentityUser<int>
    {
        public string Name { get; set; }
    }
}
