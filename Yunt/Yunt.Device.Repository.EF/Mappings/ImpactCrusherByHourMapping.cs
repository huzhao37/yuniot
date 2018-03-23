
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yunt.Device.Repository.EF.Models;

namespace Yunt.Device.Repository.EF.Mappings
{
    public class ImpactCrusherByHourMapping : IEntityTypeConfiguration<ImpactCrusherByHour>
    {
        public void Configure(EntityTypeBuilder<ImpactCrusherByHour> entity)
        {
            entity.ToTable("ImpactCrusherByHour");
            entity.HasKey(c => c.Id);
            entity.HasIndex(m => m.MotorId);
        }
    }
}
