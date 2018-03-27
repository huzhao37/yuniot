
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yunt.Device.Repository.EF.Models;

namespace Yunt.Device.Repository.EF.Mappings
{
    public class PulverizerMapping : IEntityTypeConfiguration<Pulverizer>
    {
        public void Configure(EntityTypeBuilder<Pulverizer> entity)
        {
            entity.ToTable("Pulverizer");
            entity.HasKey(c => c.Id);
            
        }
    }
}
