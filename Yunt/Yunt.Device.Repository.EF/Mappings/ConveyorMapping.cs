
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
    public class ConveyorMapping : IEntityTypeConfiguration<Conveyor>
    {
        public void Configure(EntityTypeBuilder<Conveyor> entity)
        {
            entity.ToTable("Conveyor");
            entity.HasKey(c => c.Id);
            //HasRequired(c => c.Motor).WithRequiredDependent().Map(c => c.MapKey("MotorId")); 
        }
    }
}
