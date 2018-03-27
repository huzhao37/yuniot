



using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yunt.Device.Repository.EF.Models;

namespace Yunt.Device.Repository.EF.Mappings
{
    public class VerticalCrusherByDayMapping : IEntityTypeConfiguration<VerticalCrusherByDay>
    {
        public void Configure(EntityTypeBuilder<VerticalCrusherByDay> entity)
        {
            entity.ToTable("VerticalCrusherByDay");
            entity.HasKey(m => m.Id);
            
        }
    }
}
