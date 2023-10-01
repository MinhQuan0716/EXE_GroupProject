using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceRepository
{
    public interface IQuizOptionRepository:IGenericRepository<QuizOption>
    {
        Task<List<QuizOption>> GetOptionsByQuizId(Guid quizId);
        Task RemoveRangeOption(Guid quizId);
        Task<Guid> GetOptionByInterestLevel(int option,Guid careerQuizId);
    }
}
