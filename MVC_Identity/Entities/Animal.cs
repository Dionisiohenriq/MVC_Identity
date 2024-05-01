using System.ComponentModel.DataAnnotations;

namespace MVC_Identity.Entities
{
    public class Animal
    {
        public int Id { get; set; }

        [Required, MaxLength(80, ErrorMessage = "Nome não pode exceder 80 caracteres!")]
        public string Name { get; set; }
        public string Specie { get; set; }
        public int Age { get; set; }
    }
}
