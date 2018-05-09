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
    public class DatatypeMapping : IEntityTypeConfiguration<Datatype>
    {
        public void Configure(EntityTypeBuilder<Datatype> entity)
        {
            entity.ToTable("datatype");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.Bit).HasColumnType("int(11)");

            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.InByte).HasColumnType("int(11)");

            entity.Property(e => e.OutIntArray).HasColumnType("int(11)");
        }
    }
}
