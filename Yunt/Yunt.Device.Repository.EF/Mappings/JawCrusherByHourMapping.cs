
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
    public class JawCrusherByHourMapping : IEntityTypeConfiguration<JawCrusherByHour>
    {
        public void Configure(EntityTypeBuilder<JawCrusherByHour> entity)
        {
            entity.ToTable("JawCrusherByHour");
            entity.HasKey(j => j.Id);
            
        }
    }
}
