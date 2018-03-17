
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
    public class VibrosieveByDayMapping : IEntityTypeConfiguration<VibrosieveByDay>
    {
        public void Configure(EntityTypeBuilder<VibrosieveByDay> entity)
        {
            entity.ToTable("VibrosieveByDay");
            entity.HasKey(m => m.Id);
        }
    }
}
