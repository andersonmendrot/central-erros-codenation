﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CentralErros.Domain.Models;

namespace CentralErros.Infrastructure.Mappings
{
    public class EnvironmentConfiguration : IEntityTypeConfiguration<Environment>
    {
        public void Configure(EntityTypeBuilder<Environment> builder)
        {
            builder.ToTable("Environments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.HasMany(e => e.Errors)
                .WithOne(en => en.Environment)
                .HasForeignKey(en => en.EnvironmentId);
        }
    }
}
