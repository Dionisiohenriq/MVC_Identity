using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVC_Identity.Entities
{
    public class Lion : Animal
    {
        public string? BirthPlace { get; set; }
        public Lion() : base() { }

        public override void Configure(EntityTypeBuilder<Animal> builder)
        {
            base.Configure(builder);
        }
    }
}
