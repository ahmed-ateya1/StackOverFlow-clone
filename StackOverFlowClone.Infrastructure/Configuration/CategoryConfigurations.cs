using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackOverFlowClone.Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlowClone.Infrastructure.Configuration
{
    internal class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.CategoryID);

            builder.Property(x=>x.CategoryID)
                .ValueGeneratedNever();

            builder.Property(x => x.CategoryName)
                .HasColumnType("VARCHAR(200)")
                .IsRequired();

            builder.ToTable("Categories");
        }
    }
}
