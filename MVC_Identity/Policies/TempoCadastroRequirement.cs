using Microsoft.AspNetCore.Authorization;

namespace MVC_Identity.Policies
{
    public class TempoCadastroRequirement : IAuthorizationRequirement
    {
        public TempoCadastroRequirement(int tempoCadastroMinimo)
        {
            TempoCadastroMinimo = tempoCadastroMinimo;
        }

        public int TempoCadastroMinimo { get; }


    }
}
