using Microsoft.AspNetCore.Identity;

namespace MVC_Identity.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? Password { get; set; }
    }
}
