
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
    public class SimonsConeCrusherMapping : IEntityTypeConfiguration<SimonsConeCrusher>
    {
        public void Configure(EntityTypeBuilder<SimonsConeCrusher> entity)
        {
            entity.ToTable("SimonsConeCrusher");
            entity.HasKey(c => c.Id);
            entity.HasIndex(m => m.MotorId);
        }
    }
}
