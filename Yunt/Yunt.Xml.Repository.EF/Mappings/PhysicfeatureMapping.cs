using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yunt.Xml.Repository.EF.Models;


namespace Yunt.Xml.Repository.EF.Mappings
{
    public class PhysicfeatureMapping : IEntityTypeConfiguration<Physicfeature>
    {
        public void Configure(EntityTypeBuilder<Physicfeature> entity)
        {
            entity.ToTable("physicfeature");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.PhysicType).HasMaxLength(255);

            entity.Property(e => e.Time).HasColumnType("datetime");

        }
    }
}
