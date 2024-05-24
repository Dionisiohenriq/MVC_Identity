using Microsoft.AspNetCore.Authorization;

namespace MVC_Identity.Policies
{
    public class TempoCadastroHandler : AuthorizationHandler<TempoCadastroRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TempoCadastroRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "CadastradoEm"))
            {
                string? data = context.User.FindFirst(c => c.Type == "CadastradoEm")?.Value;

                DateTime dataCadastro = DateTime.Parse(data);

                var tempoCadastro = await Task.Run(() => (DateTime.Now.Date - dataCadastro.Date).TotalDays);

                var tempoEmAnos = tempoCadastro / 360;

                if (tempoEmAnos >= requirement.TempoCadastroMinimo)
                {
                    context.Succeed(requirement);
                }
                return;
            }
        }
    }
}
