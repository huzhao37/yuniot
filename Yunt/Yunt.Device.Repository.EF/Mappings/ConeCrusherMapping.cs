
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yunt.Common;
using Yunt.Device.Repository.EF.Models;

namespace Yunt.Device.Repository.EF.Mappings
{
    public class ConeCrusherMapping : IEntityTypeConfiguration<ConeCrusher>
    {
        public  void Configure(EntityTypeBuilder<ConeCrusher> entity)
        {
            entity.ToTable("ConeCrusher");
            entity.HasKey(c => c.Id);
            entity.HasIndex(m => m.MotorId);
            // etc.
        }
    }
}
