
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MVC_Identity.Services
{
    public class SeedUserClaimsInitial(UserManager<IdentityUser> userManager) : ISeedUserClaimsInitial
    {
        public async Task SeedUserClaimsAsync()
        {
            try
            {
                // User1
                IdentityUser? user1 = await userManager.FindByEmailAsync("gerente@localhost");
                if (user1 is not null)
                {
                    var claimList = (await userManager.GetClaimsAsync(user1)).Select(p => p.Type);

                    if (!claimList.Contains("CadastradoEm"))
                    {
                        var claimResult = await userManager.AddClaimAsync(user1, new Claim("CadastradoEm", "03/03/2021"));
                    }
                }

                // User2
                IdentityUser? user2 = await userManager.FindByEmailAsync("usuario@localhost");
                if (user1 is not null)
                {
                    var claimList = (await userManager.GetClaimsAsync(user1)).Select(p => p.Type);

                    if (!claimList.Contains("CadastradoEm"))
                    {
                        var claimResult = await userManager.AddClaimAsync(user1, new Claim("CadastradoEm", "01/01/2020"));
                    }
                }

                // User2
                IdentityUser? user3 = await userManager.FindByEmailAsync("dionisiohenriq@localhost");
                if (user1 is not null)
                {
                    var claimList = (await userManager.GetClaimsAsync(user1)).Select(p => p.Type);

                    if (!claimList.Contains("CadastradoEm"))
                    {
                        var claimResult = await userManager.AddClaimAsync(user1, new Claim("CadastradoEm", "02/02/2017"));
                    }
                }
            }
            catch (Exception e)
            {

                throw new Exception($"{e.Message}");
            }
        }
    }
}
