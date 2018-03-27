
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yunt.Device.Repository.EF.Models;

namespace Yunt.Device.Repository.EF.Mappings
{
    public class DoubleToothRollCrusherMapping : IEntityTypeConfiguration<DoubleToothRollCrusher>
    {
        public void Configure(EntityTypeBuilder<DoubleToothRollCrusher> entity)
        {
            entity.ToTable("DoubleToothRollCrusher");
            entity.HasKey(c => c.Id);
            
        }
    }
}
