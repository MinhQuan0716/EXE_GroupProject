using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel.QuizModel
{
    public  class ViewCareerQuizModel
    {
        public string Question { get; set; }
        public ICollection<int> Option { get; set; }
    }
}
