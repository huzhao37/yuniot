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
    public class DataformmodelMapping : IEntityTypeConfiguration<Dataformmodel>
    {
        public void Configure(EntityTypeBuilder<Dataformmodel> entity)
        {
            entity.ToTable("dataformmodel");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .HasColumnType("int(11)");

            entity.Property(e => e.Bit).HasColumnType("int(11)");

            entity.Property(e => e.BitDesc).HasMaxLength(50);

            entity.Property(e => e.CollectdeviceIndex)
                .HasColumnName("collectdevice_index")
                .HasMaxLength(50);

            entity.Property(e => e.DataPhysicalAccuracy).HasMaxLength(50);

            entity.Property(e => e.DataPhysicalFeature).HasMaxLength(50);

            entity.Property(e => e.DataPhysicalId).HasColumnType("int(11)");

            entity.Property(e => e.DataType).HasMaxLength(50);

            entity.Property(e => e.DebugValue).HasColumnType("int(11)");

            entity.Property(e => e.DeviceId).HasMaxLength(50);

            entity.Property(e => e.Divalue)
                .HasColumnName("DIValue")
                .HasColumnType("int(11)");

            entity.Property(e => e.Dovalue)
                .HasColumnName("DOValue")
                .HasColumnType("int(11)");

            entity.Property(e => e.FieldParam).HasMaxLength(50);

            entity.Property(e => e.FieldParamEn).HasMaxLength(50);

            entity.Property(e => e.FormatId).HasColumnType("int(11)");

            entity.Property(e => e.Index).HasColumnType("int(11)");

            entity.Property(e => e.LineId).HasColumnType("int(11)");

            entity.Property(e => e.MachineId).HasMaxLength(50);

            entity.Property(e => e.MachineName).HasMaxLength(50);

            entity.Property(e => e.MotorId).HasMaxLength(15);

            entity.Property(e => e.MotorTypeName).HasMaxLength(50);

            entity.Property(e => e.Time).HasColumnType("datetime");

            entity.Property(e => e.Unit).HasMaxLength(50);

            entity.Property(e => e.WarnValue).HasColumnType("int(11)");

        }
    }
}
