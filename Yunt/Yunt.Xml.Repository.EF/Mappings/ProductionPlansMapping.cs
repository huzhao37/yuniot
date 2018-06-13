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
    public class ProductionPlansMapping : IEntityTypeConfiguration<ProductionPlans>
    {
        public void Configure(EntityTypeBuilder<ProductionPlans> entity)
        {
            entity.ToTable("productionplans");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int(11)");

            entity.Property(e => e.End)
                .HasColumnName("end")
                .HasColumnType("int(11)");

            entity.Property(e => e.FinishCy1)
                .HasColumnName("finishcy1")
                .HasColumnType("int(11)");

            entity.Property(e => e.FinishCy2)
                .HasColumnName("finish_cy2")
                .HasColumnType("int(11)");

            entity.Property(e => e.FinishCy3)
                .HasColumnName("finishcy3")
                .HasColumnType("int(11)");

            entity.Property(e => e.FinishCy4)
                .HasColumnName("finishcy4")
                .HasColumnType("int(11)");

            entity.Property(e => e.MainCy)
                .HasColumnName("maincy")
                .HasColumnType("int(11)");

            entity.Property(e => e.ProductionlineId)
                .IsRequired()
                .HasColumnName("productionlineid")
                .HasMaxLength(50);

            entity.Property(e => e.Remark)
                .HasColumnName("remark")
                .HasMaxLength(255);

            entity.Property(e => e.Start)
                .HasColumnName("start")
                .HasColumnType("int(11)");

            entity.Property(e => e.Time)
                .HasColumnName("time")
                .HasColumnType("datetime").HasDefaultValue(DateTime.Now);
        }
    }
}
