
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
    public class MotorTypeMapping : IEntityTypeConfiguration<MotorType>
    {
        public void Configure(EntityTypeBuilder<MotorType> entity)
        {
            entity.ToTable("MotorType");
            entity.HasKey(m => m.Id);
            entity.HasIndex(m => m.MotorTypeId);
            // HasMany(m => m.Motors).WithRequired(p=>p.MotorType).WillCascadeOnDelete(false);
            // HasMany(m => m.StandParamValues).WithRequired(p => p.MotorType).WillCascadeOnDelete(false);
        }
    }
}
