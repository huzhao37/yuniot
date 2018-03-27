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
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("User");
            entity.HasKey(m => m.Id);
            entity.Property(m => m.LoginAccount).IsRequired();
            entity.Property(m => m.LoginPwd).IsRequired();
            entity.Property(m => m.UserId).IsRequired();
            entity.Property(m => m.UserRoleId).IsRequired();
            entity.Property(m => m.UserName).IsRequired();
        }
    }
}
