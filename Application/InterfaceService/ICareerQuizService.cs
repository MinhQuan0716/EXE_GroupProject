using Application.ViewModel.QuizModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceService
{
    public  interface ICareerQuizService
    {
        Task<bool> CreateQuiz(CreateCareerQuizModel createCareerQuizModel);
        Task<List<ViewCareerQuizModel >> GetAllQuiz();  
    }
}
