using Application.ViewModel.QuizModel;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceRepository
{
    public  interface ICareerQuizRepository:IGenericRepository<CareerQuiz>
    {
        Task<CareerQuiz> GetlastSavedQuiz();
        Task<List<ViewCareerQuizModel>> GetAllQuiz();
        Task RemoveQuiz(Guid id);
        Task<Guid> GetQuizIdFromQuizText(string text);
    }
}
