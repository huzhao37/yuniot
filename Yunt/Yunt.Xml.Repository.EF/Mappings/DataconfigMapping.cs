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
    public class DataconfigMapping : IEntityTypeConfiguration<Dataconfig>
    {
        public void Configure(EntityTypeBuilder<Dataconfig> entity)
        {
            entity.ToTable("dataconfig");

            entity.HasIndex(e => e.Datatype_Id)
                .HasName("IX_DataConfig_DataTypeId");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int(11)");

            entity.Property(e => e.Collectdevice_Index)
                .IsRequired()
                .HasColumnName("collectdevice_index")
                .HasMaxLength(50);

            entity.Property(e => e.Count)
                .HasColumnName("count")
                .HasColumnType("int(11)");

            entity.Property(e => e.Datatype_Id)
                .HasColumnName("datatype_id")
                .HasColumnType("int(11)");

        }
    }
}
