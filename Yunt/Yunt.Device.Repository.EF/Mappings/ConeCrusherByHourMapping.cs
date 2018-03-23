
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yunt.Device.Domain.Model;


namespace Yunt.Device.Repository.EF.Mappings
{
    public class ConeCrusherByHourMapping : IEntityTypeConfiguration<ConeCrusherByHour>
    {
        public void Configure(EntityTypeBuilder<ConeCrusherByHour> entity)
        {
            entity.ToTable("ConeCrusherByHour");
            entity.HasKey(c => c.Id);
            entity.HasIndex(m => m.MotorId);
        }
    }
}
