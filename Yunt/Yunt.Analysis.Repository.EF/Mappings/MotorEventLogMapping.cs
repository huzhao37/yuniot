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
    public class MotorEventLogMapping : IEntityTypeConfiguration<MotorEventLog>
    {
        public void Configure(EntityTypeBuilder<MotorEventLog> entity)
        {
            entity.ToTable("MotorEventLog");
            entity.HasKey(m => m.Id);
            entity.Property(m => m.EventCode).IsRequired();
            entity.Property(m => m.Description).IsRequired();
            entity.Property(m => m.MotorId).IsRequired();
            entity.Property(m => m.ProductionLineId).IsRequired();
            entity.Property(m => m.Time).IsRequired();
        }
    }
}
