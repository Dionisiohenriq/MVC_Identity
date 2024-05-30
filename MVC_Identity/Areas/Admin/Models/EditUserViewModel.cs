using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;

namespace MVC_Identity.Areas.Admin.Models
{
    public class EditUserViewModel
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public List<Claim>? Claims { get; set; }
        public EditUserViewModel()
        {
            Claims = [];
        }
    }
}
