using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;

namespace MVC_Identity.Entities
{
    public class Animal : IEntityTypeConfiguration<Animal>
    {
        public Guid Id { get; set; }

        [Required, MaxLength(80, ErrorMessage = "Nome não pode exceder 80 caracteres!")]
        public string? Name { get; set; }
        public string? Specie { get; set; }
        public int Age { get; set; }
        public bool Excluded { get; private set; }

        public Animal()
        {
        }

        public void Delete()
        {
            Excluded = true;
        }

        public virtual void Configure(EntityTypeBuilder<Animal> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().UseIdentityColumn();
            builder.Property(p => p.Age).IsRequired();
            builder.Property(n => n.Name).IsRequired().HasMaxLength(80);

        }
    }
}
