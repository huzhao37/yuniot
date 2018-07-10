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
    public class WareHousesMapping : IEntityTypeConfiguration<WareHouses>
    {
        public void Configure(EntityTypeBuilder<WareHouses> entity)
        {
            entity.ToTable("WareHouses");
            entity.HasKey(m => m.Id);
            entity.Property(m => m.MotorTypeId).IsRequired();
            entity.Property(m => m.ProductionLineId).IsRequired();
            entity.Property(m => m.CreateTime).IsRequired();
            entity.Property(m => m.Name).IsRequired();
            entity.Property(m => m.Keeper).IsRequired();
        }
    }
}
