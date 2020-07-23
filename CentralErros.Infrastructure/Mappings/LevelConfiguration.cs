using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CentralErros.Domain.Models;

namespace CentralErros.Infrastructure.Mappings
{
    public class LevelConfiguration : IEntityTypeConfiguration<Level>
    {
        public void Configure(EntityTypeBuilder<Level> builder)
        {
            builder.ToTable("Levels");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.HasMany(e => e.Errors)
                .WithOne(l => l.Level)
                .HasForeignKey(l => l.LevelId);
        }
    }
}
