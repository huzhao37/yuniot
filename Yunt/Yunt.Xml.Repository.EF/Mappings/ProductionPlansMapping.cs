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
            entity.ToTable("production_plans");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int(11)");

            entity.Property(e => e.End)
                .HasColumnName("end")
                .HasColumnType("int(11)");

            entity.Property(e => e.Finish_Cy1)
                .HasColumnName("finish_cy1")
                .HasColumnType("int(11)");

            entity.Property(e => e.Finish_Cy2)
                .HasColumnName("finish_cy2")
                .HasColumnType("int(11)");

            entity.Property(e => e.Finish_Cy3)
                .HasColumnName("finish_cy3")
                .HasColumnType("int(11)");

            entity.Property(e => e.Finish_Cy4)
                .HasColumnName("finish_cy4")
                .HasColumnType("int(11)");

            entity.Property(e => e.Main_Cy)
                .HasColumnName("main_cy")
                .HasColumnType("int(11)");

            entity.Property(e => e.Productionline_Id)
                .IsRequired()
                .HasColumnName("productionline_id")
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
