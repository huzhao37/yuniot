using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yunt.Auth.Repository.EF.Models.IdModel;


namespace Yunt.Auth.Repository.EF.Mappings
{
    public class UserIdFactoriesMapping : IEntityTypeConfiguration<UserIdFactories>
    {
        public void Configure(EntityTypeBuilder<UserIdFactories> entity)
        {
            entity.ToTable("UserIdFactories");
            entity.HasKey(m => m.Id);
            entity.Property(m => m.UserIndex).IsRequired();
            entity.Property(m => m.Time).IsRequired();

     
        }
    }
}
