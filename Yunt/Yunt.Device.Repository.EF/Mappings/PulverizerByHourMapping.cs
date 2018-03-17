
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
    public class PulverizerByHourMapping : IEntityTypeConfiguration<PulverizerByHour>
    {
        public void Configure(EntityTypeBuilder<PulverizerByHour> entity)
        {
            entity.ToTable("PulverizerByHour");
            entity.HasKey(c => c.Id);
        }
    }
}
