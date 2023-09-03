using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class QuizOption:BaseEntity
    {
        public int InterestLevel { get; set; }
        public Guid? CareerQuiz { get; set; }
        public CareerQuiz Career { get; set; }
        public ICollection<UserResponse> Responses { get; set; }
    }
}
