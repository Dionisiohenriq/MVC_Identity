using System.ComponentModel.DataAnnotations;

namespace MVC_Identity.Models
{
    public class RegisterViewModel
    {
        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Confirme a senha"), Compare("Password", ErrorMessage = "As senhas não estão iguais.")]
        public string? ConfirmPassword { get; set; }
    }
}
