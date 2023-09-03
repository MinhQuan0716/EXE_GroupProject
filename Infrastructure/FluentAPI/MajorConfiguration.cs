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
    public class MajorConfiguration : IEntityTypeConfiguration<Major>
    {
        public void Configure(EntityTypeBuilder<Major> builder)
        {
            builder.HasData(new Major
            {
                MajorId= 1,
                MajorName = "Software Engineer",
                MajorDescription="SE"
            },
              new Major
              {
                 MajorId= 2,
                  MajorName = "Graphic Design",
                  MajorDescription=""
              },
              new Major
              {
                  MajorId= 3,
                  MajorName="Digital Marketing",
                  MajorDescription=""
              }
              );
        }
    }
}
