using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yunt.Inventory.Repository.EF.Models.IdModel;


namespace Yunt.Inventory.Repository.EF.Mappings
{
    public class SparePartsIdFactoriesMapping : IEntityTypeConfiguration<SparePartsIdFactories>
    {
        public void Configure(EntityTypeBuilder<SparePartsIdFactories> entity)
        {
            entity.ToTable("SparePartsIdFactories");
            entity.HasKey(m => m.Id);
            entity.Property(m => m.SparePartsIndex).IsRequired();
            entity.Property(m => m.SparePartsTypeId).IsRequired();

     
        }
    }
}
