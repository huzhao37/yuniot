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
    public class EventKindMapping : IEntityTypeConfiguration<EventKind>
    {
        public void Configure(EntityTypeBuilder<EventKind> entity)
        {
            entity.ToTable("EventKind");
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Description).IsRequired();
            entity.Property(m => m.Code).IsRequired();
            entity.Property(m => m.Regulation).IsRequired();
            entity.Property(m => m.MotorTypeId).IsRequired();
            entity.Property(m => m.Time).IsRequired();
        }
    }
}
