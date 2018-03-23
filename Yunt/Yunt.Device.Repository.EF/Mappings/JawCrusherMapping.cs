
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
    class JawCrusherMapping : IEntityTypeConfiguration<JawCrusher>
    {
        public void Configure(EntityTypeBuilder<JawCrusher> entity)
        {
            entity.ToTable("JawCrusher");
            entity.HasKey(j => j.Id);
            entity.HasIndex(m => m.MotorId);
            //HasRequired(j => j.Motor).WithRequiredDependent().Map(c => c.MapKey("MotorId")).WillCascadeOnDelete(false);
        }
    }
}
