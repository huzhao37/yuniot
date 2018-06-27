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
    public class MaintainMapping : IEntityTypeConfiguration<Maintain>
    {
        public void Configure(EntityTypeBuilder<Maintain> entity)
        {
            entity.ToTable("Maintain");
            entity.HasKey(m => m.Id);
            entity.Property(m => m.MotorId).IsRequired();
            entity.Property(m => m.Duration).IsRequired();
            entity.Property(m => m.Time).IsRequired();
        }
    }
}
