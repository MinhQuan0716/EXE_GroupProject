using Application.ViewModel.QuizModel;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceService
{
    public  interface IUserResopnseService
    {
        Task<SuggestionViewModel> DoingQuiz(List<CreateResponseViewModel> createResponseViewModels);
        Task<List<PointViewModel>> SeePoints();
    }
}
