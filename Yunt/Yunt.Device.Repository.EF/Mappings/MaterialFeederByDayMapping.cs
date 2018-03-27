
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yunt.Device.Repository.EF.Models;

namespace Yunt.Device.Repository.EF.Mappings
{
    public class MaterialFeederByDayMapping : IEntityTypeConfiguration<MaterialFeederByDay>
    {
        public void Configure(EntityTypeBuilder<MaterialFeederByDay> entity)
        {
            entity.ToTable("MaterialFeederByDay");
            entity.HasKey(m => m.Id);
            
        }
    }
}