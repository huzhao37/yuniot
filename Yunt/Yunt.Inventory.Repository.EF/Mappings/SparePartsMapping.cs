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
    public class SparePartsMapping : IEntityTypeConfiguration<SpareParts>
    {
        public void Configure(EntityTypeBuilder<SpareParts> entity)
        {
            entity.ToTable("SpareParts");
            entity.HasKey(m => m.Id);
            entity.Property(m => m.SparePartsId).IsRequired();
            entity.Property(m => m.SparePartsTypeId).IsRequired();
            entity.Property(m => m.MotorId).IsRequired();
            entity.Property(m => m.WareHousesId).IsRequired();
        }
    }
}
