
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
    public class VibrosieveByHourMapping : IEntityTypeConfiguration<VibrosieveByHour>
    {
        public void Configure(EntityTypeBuilder<VibrosieveByHour> entity)
        {
            entity.ToTable("VibrosieveByHour");
            entity.HasKey(m => m.Id);
            
        }
    }
}
