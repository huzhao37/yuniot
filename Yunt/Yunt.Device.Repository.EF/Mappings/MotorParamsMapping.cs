using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yunt.Device.Repository.EF.Models;

namespace Yunt.Device.Repository.EF.Mappings
{
    public class MotorParamsMapping : IEntityTypeConfiguration<MotorParams>
    {
        public void Configure(EntityTypeBuilder<MotorParams> entity)
        {
            entity.ToTable("MotorParams");
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Description).HasMaxLength(120).IsRequired();

        }
    }
}
