using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FluentAPI
{
    public class SuggestionConfiguration : IEntityTypeConfiguration<Suggestion>
    {
        public void Configure(EntityTypeBuilder<Suggestion> builder)
        {
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.suggestionContent).HasColumnType("nvarchar(max)");
            builder.HasOne(x=>x.User).WithMany(x=>x.Suggestions).HasForeignKey(x=>x.UserId);
            builder.HasOne(x => x.Major).WithMany(x => x.Suggestions).HasForeignKey(x => x.MajorId);
        }
    }
}
