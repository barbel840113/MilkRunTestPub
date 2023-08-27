using Microsoft.EntityFrameworkCore;

using MilkRun.ApplicationCore.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MilkRun.Infrastructure.DbContexts.ModelConfiguration
{
    public class ProductModelBuilder: IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {            
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Title).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(100);
            builder.Property(p => p.Price).IsRequired();
            builder.HasOne(p => p.Brand)
                .WithMany(x => x.Products)
                .HasForeignKey(p => p.BrandId)
                .IsRequired(true);

            builder.HasIndex(a => new { a.Title, a.BrandId })
                .HasDatabaseName("IX_Product_Title_BrandId");
        }
    }
}
