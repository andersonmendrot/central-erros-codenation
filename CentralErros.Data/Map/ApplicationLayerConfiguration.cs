using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CentralErros.Domain;

namespace CentralErros.Data.Map
{
    public class ApplicationLayerConfiguration : IEntityTypeConfiguration<ApplicationLayer>
    {
        public void Configure(EntityTypeBuilder<ApplicationLayer> builder)
        {
           
        }
    }
}
