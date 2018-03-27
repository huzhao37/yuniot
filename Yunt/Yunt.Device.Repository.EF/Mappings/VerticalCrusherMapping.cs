

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yunt.Device.Repository.EF.Models;

namespace Yunt.Device.Repository.EF.Mappings
{
    public class VerticalCrusherMapping : IEntityTypeConfiguration<VerticalCrusher>
    {
        public void Configure(EntityTypeBuilder<VerticalCrusher> entity)
        {
            entity.ToTable("VerticalCrusher");
            entity.HasKey(m => m.Id);

            
            //HasRequired(m => m.Motor).WithRequiredDependent().Map(c => c.MapKey("MotorId")).WillCascadeOnDelete(false);
        }
    }
}
