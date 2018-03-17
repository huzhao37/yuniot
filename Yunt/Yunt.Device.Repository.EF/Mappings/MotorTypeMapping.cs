
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
    public class MotorTypeMapping : IEntityTypeConfiguration<Motortype>
    {
        public void Configure(EntityTypeBuilder<Motortype> entity)
        {
            entity.ToTable("MotorType");
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Name).HasMaxLength(120).IsRequired();
            entity.Property(m => m.Code).HasMaxLength(120).IsRequired();

           // HasMany(m => m.Motors).WithRequired(p=>p.MotorType).WillCascadeOnDelete(false);
           // HasMany(m => m.StandParamValues).WithRequired(p => p.MotorType).WillCascadeOnDelete(false);
        }
    }
}
