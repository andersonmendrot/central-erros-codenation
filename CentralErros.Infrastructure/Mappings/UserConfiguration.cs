using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CentralErros.Domain.Models;

namespace CentralErros.Infrastructure.Mappings
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnType("varchar(254)")
                .IsRequired();

            builder.Property(x => x.Password)
                .HasColumnType("varchar(15)")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("datetime")
                .IsRequired();
        }
    }
}
