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
    public class MessagequeueMapping : IEntityTypeConfiguration<Messagequeue>
    {
        public void Configure(EntityTypeBuilder<Messagequeue> entity)
        {
            entity.ToTable("messagequeue");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int(11)");

            entity.Property(e => e.Collectdevice_Index)
                .IsRequired()
                .HasColumnName("collectdevice_index")
                .HasMaxLength(20);

            entity.Property(e => e.Com_Type)
                .IsRequired()
                .HasColumnName("com_type")
                .HasMaxLength(50);

            entity.Property(e => e.Host)
                .IsRequired()
                .HasColumnName("host")
                .HasMaxLength(50);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(20);

            entity.Property(e => e.Port)
                .HasColumnName("port")
                .HasColumnType("int(11)");

            entity.Property(e => e.Pwd)
                .IsRequired()
                .HasColumnName("pwd")
                .HasMaxLength(255);

            entity.Property(e => e.Remark)
                .HasColumnName("remark")
                .HasMaxLength(255);

            entity.Property(e => e.Route_Key)
                .IsRequired()
                .HasColumnName("route_key")
                .HasMaxLength(20);

            entity.Property(e => e.Time)
                .HasColumnName("time")
                .HasColumnType("datetime");

            entity.Property(e => e.Timer)
                .HasColumnName("timer")
                .HasColumnType("int(11)");

            entity.Property(e => e.Username)
                .IsRequired()
                .HasColumnName("username")
                .HasMaxLength(255);

            entity.Property(e => e.Write_Read)
                .HasColumnName("write_read")
                .HasColumnType("int(10)");
        }
    }
}
