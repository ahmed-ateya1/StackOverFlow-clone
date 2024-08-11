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
    internal class AnswerConfigurations : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(x=>x.AnswerID);

            builder.Property(x=>x.AnswerID)
                .ValueGeneratedNever();

            builder.Property(x => x.AnswerText)
                .HasColumnType("VARCHAR(MAX)")
                .IsRequired();

            builder.Property(x => x.AnswerDateAndTime)
                .IsRequired();

            builder.Property(x=>x.VotesCount)
                .IsRequired();

            builder.HasOne(x=>x.Question)
                .WithMany(x=>x.Answers)
                .HasForeignKey(x=>x.QuestionID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x=>x.User)
                .WithMany(x=>x.Answers)
                .HasForeignKey(x=>x.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Answers");
        }
    }
}
