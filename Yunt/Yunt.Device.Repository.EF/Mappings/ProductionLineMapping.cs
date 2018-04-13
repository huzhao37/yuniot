﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yunt.Device.Repository.EF.Models;


namespace Yunt.Device.Repository.EF.Mappings
{
    public class ProductionLineMapping : IEntityTypeConfiguration<ProductionLine>
    {
        public void Configure(EntityTypeBuilder<ProductionLine> entity)
        {
            entity.ToTable("ProductionLine");
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Name).HasMaxLength(120).IsRequired();
            

            //this.HasMany(a => a.EmbeddedDevices).WithMany(a => a.Motors).Map(m =>
            //{
            //    m.MapLeftKey("MotorId");
            //    m.MapRightKey("EmbeddedDeviceId");
            //    m.ToTable("EmbeddedDevices_Motors");
            //});
        }
    }
}