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
    public class SparePartsTypeMapping : IEntityTypeConfiguration<SparePartsType>
    {
        public void Configure(EntityTypeBuilder<SparePartsType> entity)
        {
            entity.ToTable("SparePartsType");
            entity.HasKey(m => m.Id);
            //entity.Property(m => m.SparePartsTypeId).IsRequired();
            entity.Property(m => m.CreateTime).IsRequired();
        }
    }
}
