using Microsoft.AspNetCore.Authorization;

namespace MVC_Identity.Policies
{
    public class TempoCadastroRequirement(int tempoCadastroMinimo) : IAuthorizationRequirement
    {
        public int TempoCadastroMinimo { get; } = tempoCadastroMinimo;


    }
}
