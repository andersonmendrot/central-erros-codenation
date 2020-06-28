using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CentralErros.Domain;

namespace CentralErros.Data.Map
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

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

            builder.Property(x => x.Timestamp)
                .HasColumnName("CreatedAt")
                .HasColumnType("timestamp")
                .IsRequired();
        }
    }
}
