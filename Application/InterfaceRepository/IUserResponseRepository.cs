using Application.ViewModel.QuizModel;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceRepository
{
    public  interface IUserResponseRepository:IGenericRepository<UserResponse>
    {
        Task DeleteResponse(Guid id);
        Task<List<PointViewModel>> CalculatePoint(Guid userId,int amountQuiz);

    }
}
