using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CentralErros.Domain.Models;

namespace CentralErros.Infrastructure.Mappings
{
    public class ErrorConfiguration : IEntityTypeConfiguration<Error>
    {
        public void Configure(EntityTypeBuilder<Error> builder)
        {
            builder.ToTable("Errors");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.Details)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.Origin)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder.Property(x => x.Status)
                .HasColumnType("char")
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(x => x.NumberEvents)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasCheckConstraint(
                        "constraint_status",
                        "Status = 'y' or Status = 'n'");
        }
    }
}
