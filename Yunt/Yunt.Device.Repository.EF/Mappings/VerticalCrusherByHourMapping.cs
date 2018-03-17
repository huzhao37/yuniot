



using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yunt.Device.Repository.EF.Models;

namespace Yunt.Device.Repository.EF.Mappings
{
    public class VerticalCrusherByHourMapping : IEntityTypeConfiguration<VerticalCrusherByHour>
    {
        public void Configure(EntityTypeBuilder<VerticalCrusherByHour> entity)
        {
            entity.ToTable("VerticalCrusherByHour");
            entity.HasKey(m => m.Id);
        }
    }
}
