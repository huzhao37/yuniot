using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yunt.Analysis.Repository.EF.Models;


namespace Yunt.Analysis.Repository.EF.Mappings
{
    public class AlarmInfoMapping : IEntityTypeConfiguration<AlarmInfo>
    {
        public void Configure(EntityTypeBuilder<AlarmInfo> entity)
        {
            entity.ToTable("AlarmInfo");
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Content).IsRequired();
            entity.Property(m => m.MotorName).IsRequired();
            entity.Property(m => m.Time).IsRequired();
        }
    }
}
