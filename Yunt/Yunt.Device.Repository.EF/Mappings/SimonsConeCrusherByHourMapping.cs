
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
    public class SimonsConeCrusherByHourMapping : IEntityTypeConfiguration<SimonsConeCrusherByHour>
    {
        public void Configure(EntityTypeBuilder<SimonsConeCrusherByHour> entity)
        {
            entity.ToTable("SimonsConeCrusherByHour");
            entity.HasKey(c => c.Id);
            
        }
    }
}
