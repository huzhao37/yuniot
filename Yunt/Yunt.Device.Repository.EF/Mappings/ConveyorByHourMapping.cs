
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
    public class ConveyorByHourMapping : IEntityTypeConfiguration<ConveyorByHour>
    {
        public void Configure(EntityTypeBuilder<ConveyorByHour> entity)
        {
            entity.ToTable("ConveyorByHour");
            entity.HasKey(c => c.Id);
            entity.HasIndex(m => m.MotorId);
        }
    }
}
