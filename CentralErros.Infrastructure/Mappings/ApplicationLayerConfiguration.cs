using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CentralErros.Domain.Models;

namespace CentralErros.Infrastructure.Mappings
{
    public class ApplicationLayerConfiguration : IEntityTypeConfiguration<ApplicationLayer>
    {
        public void Configure(EntityTypeBuilder<ApplicationLayer> builder)
        {
            builder.ToTable("ApplicationLayer");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.HasMany<Error>(e => e.Errors)
                .WithOne(a => a.ApplicationLayer)
                .HasForeignKey(a => a.ApplicationLayerId);  
        }
    }
}
