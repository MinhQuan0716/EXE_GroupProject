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
    public class UserResponseConfigurartion : IEntityTypeConfiguration<UserResponse>
    {
        public void Configure(EntityTypeBuilder<UserResponse> builder)
        {
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.HasOne(x => x.User).WithMany(x => x.UserResponses).HasForeignKey(x => x.UserId);
            builder.HasOne(x=>x.SelectOption).WithMany(x=>x.Responses).HasForeignKey(x => x.SelectedOptionId);
        }
    }
}
