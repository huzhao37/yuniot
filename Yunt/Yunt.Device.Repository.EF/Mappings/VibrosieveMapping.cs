


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yunt.Device.Repository.EF.Models;

namespace Yunt.Device.Repository.EF.Mappings
{
    public class VibrosieveMapping : IEntityTypeConfiguration<Vibrosieve>
    {
        public void Configure(EntityTypeBuilder<Vibrosieve> entity)
        {
            entity.ToTable("Vibrosieve");
            entity.HasKey(m => m.Id);
            entity.HasIndex(m => m.MotorId);
            //HasRequired(m => m.Motor).WithRequiredDependent().Map(c => c.MapKey("MotorId")).WillCascadeOnDelete(false);
        }
    }
}
