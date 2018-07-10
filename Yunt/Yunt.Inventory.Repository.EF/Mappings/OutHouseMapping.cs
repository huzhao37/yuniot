using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yunt.Inventory.Repository.EF.Models;


namespace Yunt.Inventory.Repository.EF.Mappings
{
    public class OutHouseMapping : IEntityTypeConfiguration<OutHouse>
    {
        public void Configure(EntityTypeBuilder<OutHouse> entity)
        {
            entity.ToTable("OutHouse");
            entity.HasKey(m => m.Id);
            entity.Property(m => m.SparePartsTypeId).IsRequired();
            entity.Property(m => m.BatchNo).IsRequired();
            entity.Property(m => m.WareHousesId).IsRequired();
            entity.Property(m => m.MotorId).IsRequired();
        }
    }
}
