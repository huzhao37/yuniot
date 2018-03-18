using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yunt.Device.Repository.EF.Models.IdModel;


namespace Yunt.Device.Repository.EF.Mappings
{
    public class MotorIdFactoriesMapping : IEntityTypeConfiguration<MotorIdFactories>
    {
        public void Configure(EntityTypeBuilder<MotorIdFactories> entity)
        {
            entity.ToTable("MotorIdFactories");
            entity.HasKey(m => m.Id);
            entity.Property(m => m.ProductionLineId).HasMaxLength(120).IsRequired();
            entity.Property(m => m.MotorTypeId).HasMaxLength(120).IsRequired();

     
        }
    }
}
