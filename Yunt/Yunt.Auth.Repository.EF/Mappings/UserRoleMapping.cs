using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yunt.Auth.Repository.EF.Models;


namespace Yunt.Auth.Repository.EF.Mappings
{
    public class UserRoleMapping : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> entity)
        {
            entity.ToTable("UserRole");
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Desc).IsRequired();
            entity.Property(m => m.Remark).IsRequired();
        }
    }
}
