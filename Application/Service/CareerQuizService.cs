using Application.InterfaceService;
using Application.ViewModel.QuizModel;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class CareerQuizService : ICareerQuizService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CareerQuizService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateQuiz(CreateCareerQuizModel createCareerQuizModel)
        {
            int typeId = await _unitOfWork.QuizTypeRepository.GetTypeIdByName(createCareerQuizModel.QuizType.ToString());
            var question = new CareerQuiz
            {
                QuizText=createCareerQuizModel.QuizText,
                TypeId=typeId,
                IsDelete=false,
            };
            await _unitOfWork.CareerQuizRepository.AddAsync(question);
            await _unitOfWork.SaveChangeAsync();
            await _unitOfWork.CareerQuizRepository.GetlastSavedQuiz();
            List<QuizOption> options = new List<QuizOption>();
            QuizOption quizOption1 = new QuizOption
            {
                InterestLevel = 1,
                CareerQuizId = question.Id,
            };
            QuizOption quizOption2 = new QuizOption
            {
                InterestLevel = 2,
                CareerQuizId = question.Id,
            };
            QuizOption quizOption3 = new QuizOption
            {
                InterestLevel = 3,
                CareerQuizId = question.Id,
            };
            QuizOption quizOption4 = new QuizOption
            {
                InterestLevel = 4,
                CareerQuizId = question.Id,
            };
            QuizOption quizOption5 = new QuizOption
            {
                InterestLevel = 5,
                CareerQuizId = question.Id,
            };
            options.Add(quizOption1);
            options.Add(quizOption2);
            options.Add(quizOption3);
            options.Add(quizOption4);
            options.Add(quizOption5);
            await  _unitOfWork.QuizOptionRepository.AddRangeAsync(options);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<List<ViewCareerQuizModel>> GetAllQuiz()
        {
            return await _unitOfWork.CareerQuizRepository.GetAllQuiz();
        }

        /*public async Task<QuizOption> GetQuizOptionForUserResponse(Guid userResponseId)
        {
            var userResponse = await _unitOfWork.UserResponseRepository.GetUserResponseById(userResponseId);

            if (userResponse != null)
            {
                var selectedOptionId = userResponse.SelectedOptionId;

                if (selectedOptionId.HasValue)
                {
                    var quizOption = await _unitOfWork.QuizOptionRepository.GetOptionById(selectedOptionId.Value);

                    if (quizOption != null)
                    {
                        return quizOption;
                    }
                }
            }

            return null; 
        }*/
    }
}
