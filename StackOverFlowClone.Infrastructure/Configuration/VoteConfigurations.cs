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
    public class VoteConfigurations : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.HasKey(x => x.VoteID);

            builder.Property(x=>x.VoteID)
                .ValueGeneratedNever();

            builder.Property(x=>x.VoteValue)
                .IsRequired();  

            builder.HasOne(x=>x.User)
                .WithMany(x=>x.Votes)
                .HasForeignKey(x=>x.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x=>x.Answer)
                .WithMany(x=>x.Votes)
                .HasForeignKey(x=>x.AnswerID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Votes");
        }
    }
}
