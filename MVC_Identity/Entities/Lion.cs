using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVC_Identity.Entities
{
    public class Lion : Animal
    {
        public string? BirthPlace { get; set; }
        public Lion() : base() { }

        public Lion(string? birthPlace)
        {
            BirthPlace = birthPlace;
        }

        public void Configure(EntityTypeBuilder<Lion> builder)
        {
            builder.HasBaseType(typeof(Animal));
            builder.HasKey(e => e.Id);
        }
    }
}
