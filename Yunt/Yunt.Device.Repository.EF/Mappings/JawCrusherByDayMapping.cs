
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
    public class JawCrusherByDayMapping : IEntityTypeConfiguration<JawCrusherByDay>
    {
        public void Configure(EntityTypeBuilder<JawCrusherByDay> entity)
        {
            entity.ToTable("JawCrusherByDay");
            entity.HasKey(j => j.Id);
            
        }
    }
}
