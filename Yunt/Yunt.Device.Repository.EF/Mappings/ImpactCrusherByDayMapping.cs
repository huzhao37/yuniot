
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
    public class ImpactCrusherByDayMapping : IEntityTypeConfiguration<ImpactCrusher>
    {
        public void Configure(EntityTypeBuilder<ImpactCrusher> entity)
        {
            entity.ToTable("ImpactCrusherByDay");
            entity.HasKey(c => c.Id);
        }
    }
}
