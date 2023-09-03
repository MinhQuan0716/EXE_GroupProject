using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class CareerQuiz:BaseEntity
    {
        public string QuizText { get; set; }
        public ICollection<QuizOption> QuizOptions { get; set; }
    }
}
