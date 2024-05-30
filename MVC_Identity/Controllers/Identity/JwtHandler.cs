using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MVC_Identity.Entities.Identity;
using MVC_Identity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MVC_Identity.Controllers.Identity
{
    public class JwtHandler
    {
        private readonly UserManager<ApplicationUser>? _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser>? _signInManager;

        public JwtHandler(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<object> CreateToken([FromBody] LoginViewModel loginModel)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(loginModel.Email);

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);

            if (user is not null && result.Succeeded)
            {
                Claim[]? claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                SymmetricSecurityKey? key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                SigningCredentials? creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                JwtSecurityToken token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                    _configuration["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                    );

                var jwtToken = new { token = new JwtSecurityTokenHandler().WriteToken(token) };

                return jwtToken;
            }
            return string.Empty;
        }
    }
}
