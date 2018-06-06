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
    public class CollectdeviceMapping : IEntityTypeConfiguration<Collectdevice>
    {
        public void Configure(EntityTypeBuilder<Collectdevice> entity)
        {
            entity.ToTable("collectdevice");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int(11)");

            entity.Property(e => e.Index)
                .IsRequired()
                .HasColumnName("index")
                .HasMaxLength(50);

            entity.Property(e => e.Productionline_Id)
                .IsRequired()
                .HasColumnName("productionline_id")
                .HasMaxLength(50);

            entity.Property(e => e.Remark).HasField("remark")
                .HasMaxLength(255);

            entity.Property(e => e.Time)
                .HasColumnName("time")
                .HasColumnType("datetime");
        }
    }
}
