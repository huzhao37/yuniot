
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yunt.Device.Repository.EF.Models;


namespace Yunt.Device.Repository.EF.Mappings
{
    public class MaterialFeederMapping : IEntityTypeConfiguration<MaterialFeeder>
    {
        public void Configure(EntityTypeBuilder<MaterialFeeder> entity)
        {
            entity.ToTable("MaterialFeeder");
            entity.HasKey(m => m.Id);
            //HasRequired(m => m.Motor).WithRequiredDependent().Map(c => c.MapKey("MotorId")).WillCascadeOnDelete(false); 
        }
    }
}