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
    public class QuestionConfigurations : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(x => x.QuestionID);
            builder.Property(x => x.QuestionID)
                .ValueGeneratedNever();

            builder.Property(x => x.QuestionName)
                .HasColumnType("VARCHAR(max)")
                .IsRequired();

            builder.Property(x => x.QuestionDateAndTime)
                .IsRequired();

            builder.Property(x => x.VotesCount)
                .IsRequired();

            builder.Property(x=>x.AnswersCount)
                .IsRequired();

            builder.Property(x=>x.ViewCount)
                .IsRequired();

            builder.HasOne(x=>x.User)
                .WithMany(x=>x.Questions)
                .HasForeignKey(x=>x.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x=>x.Category)
                .WithMany(x=>x.Questions)
                .HasForeignKey(x=>x.CategoryID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Questions");
        }
    }
}
