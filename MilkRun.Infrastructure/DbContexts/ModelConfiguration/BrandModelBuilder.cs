using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MilkRun.ApplicationCore.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkRun.Infrastructure.DbContexts.ModelConfiguration
{
    public class BrandModelBuilder : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasMany(p => p.Products)
                .WithOne(x => x.Brand)
                .HasForeignKey(p => p.BrandId)
                .IsRequired(true);

            builder.HasIndex(a => new { a.Name })
                .HasDatabaseName("IX_Brand_Title_BrandId");
        }
    }
}
