



using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yunt.Device.Domain.Model;

namespace Yunt.Device.Repository.EF.Mappings
{
    public class MaterialFeederByHourMapping : IEntityTypeConfiguration<MaterialFeederByHour>
    {
        public void Configure(EntityTypeBuilder<MaterialFeederByHour> entity)
        {
            entity.ToTable("MaterialFeederByHour");
            entity.HasKey(m => m.Id);
        }
    }
}