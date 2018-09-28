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
    public class InHouseMapping : IEntityTypeConfiguration<InHouse>
    {
        public void Configure(EntityTypeBuilder<InHouse> entity)
        {
            entity.ToTable("InHouse");
            entity.HasKey(m => m.Id);
            entity.Property(m => m.SparePartsTypeId).IsRequired();
            //entity.Property(m => m.BatchNo).IsRequired();
            entity.Property(m => m.WareHousesId).IsRequired();
            //entity.Property(m => m.ModelNo).HasMaxLength(25);
        }
    }
}
