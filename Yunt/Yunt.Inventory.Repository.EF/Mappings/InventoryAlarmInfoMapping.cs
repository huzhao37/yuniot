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
    public class InventoryAlarmInfoMapping : IEntityTypeConfiguration<InventoryAlarmInfo>
    {
        public void Configure(EntityTypeBuilder<InventoryAlarmInfo> entity)
        {
            entity.ToTable("InventoryAlarmInfo");
            entity.HasKey(m => m.Id);
            entity.Property(m => m.SparePartsId).IsRequired();
            entity.Property(m => m.InventoryBalance).IsRequired();
            entity.Property(m => m.CreateTime).IsRequired();

            //this.HasMany(a => a.EmbeddedInventorys).WithMany(a => a.Motors).Map(m =>
            //{
            //    m.MapLeftKey("MotorId");
            //    m.MapRightKey("EmbeddedInventoryId");
            //    m.ToTable("EmbeddedInventorys_Motors");
            //});
        }
    }
}
