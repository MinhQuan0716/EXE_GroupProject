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
        public Guid? CareerQuizId { get; set; }
        public CareerQuiz CareerQuiz { get; set; }
        public ICollection<UserResponse> Responses { get; set; }
        
    }
}
