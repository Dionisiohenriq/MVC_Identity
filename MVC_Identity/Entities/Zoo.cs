using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVC_Identity.Entities
{
    public class Zoo : IEntityTypeConfiguration<Zoo>
    {
        public Guid Id { get; private set; }
        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public DateTime Agenda { get; private set; }

        public void Configure(EntityTypeBuilder<Zoo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired();
        }
    }
}
