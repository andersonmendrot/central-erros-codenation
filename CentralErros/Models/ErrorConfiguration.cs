using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CentralErros.Models
{
    public class ErrorConfiguration : IEntityTypeConfiguration<Error>
    {
        public void Configure(EntityTypeBuilder<Error> builder)
        {
            builder.HasKey(x => new { x.LevelId, x.EnvironmentId, x.ApplicationLayerId });
            builder.HasCheckConstraint(
                        "constraint_status",
                        "status = 'y' or status = 'n'");
        }
    }
}
