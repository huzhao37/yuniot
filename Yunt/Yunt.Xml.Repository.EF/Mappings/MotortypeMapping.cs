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
    public class MotortypeMapping : IEntityTypeConfiguration<Motortype>
    {
        public void Configure(EntityTypeBuilder<Motortype> entity)
        {
            entity.ToTable("motortype");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.MotorTypeId).HasMaxLength(255);

            entity.Property(e => e.MotorTypeName).HasMaxLength(255);

            entity.Property(e => e.Time).HasColumnType("datetime");

        }
    }
}
