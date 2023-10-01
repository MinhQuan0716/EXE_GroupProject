using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FluentAPI
{
    public class QuizTypeConfiguration : IEntityTypeConfiguration<QuizType>
    {
        public void Configure(EntityTypeBuilder<QuizType> builder)
        {
            builder.HasData(new QuizType
            {
               TypeId=1,
               TypeName= nameof(QuizTypeEnum.Realistic),
            },
             new QuizType
             {
                 TypeId = 2,
                 TypeName = nameof(QuizTypeEnum.Artistic),
             },
             new QuizType
             {
                 TypeId = 3,
                 TypeName = nameof(QuizTypeEnum.Conventional),
             }
             );
        }
    }
}
