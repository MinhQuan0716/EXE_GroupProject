using Application.InterfaceService;
using Application.ViewModel.QuizModel;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class UserResponseService : IUserResopnseService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IClaimService _claimService;
        public UserResponseService(IUnitOfWork unitOfWork,IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _claimService = claimService;
        }
        public async Task<SuggestionViewModel> DoingQuiz(List<CreateResponseViewModel> createResponseViewModels)
        {
            List<CareerQuiz> listQuizzes= await _unitOfWork.CareerQuizRepository.GetAllAsync();
            int amountQuiz = listQuizzes.Count();
           foreach(var item in createResponseViewModels)
            {
                Guid quizId = await _unitOfWork.CareerQuizRepository.GetQuizIdFromQuizText(item.QuestionText);
                Guid optionId= await _unitOfWork.QuizOptionRepository.GetOptionByInterestLevel(item.Option,quizId);
                UserResponse userResponse = new UserResponse()
                {
                    UserId = _claimService.GetCurrentUserId,
                    SelectedOptionId= optionId,
                    IsDelete= false,
                };
                await _unitOfWork.UserResponse.AddAsync(userResponse);
            }
             await _unitOfWork.SaveChangeAsync();
            List<PointViewModel> userPoints = await _unitOfWork.UserResponse.CalculatePoint(_claimService.GetCurrentUserId,amountQuiz);
            PointViewModel pointTemp = new PointViewModel();
            for(int i = 0; i< userPoints.Count; i++)
            {
                for (int j = 0; j < userPoints.Count - 1; j++)
                {
                    if (userPoints[i].TotalPoint > userPoints[j + 1].TotalPoint)
                    {
                       pointTemp= userPoints[i];
                        userPoints[i] = userPoints[j+1];
                        userPoints[j+1] = pointTemp;
                    }
                    if (userPoints[i].TotalPoint == userPoints[j + 1].TotalPoint)
                    {
                        pointTemp= userPoints[j+1];
                    }
                }
            }
            Suggestion suggestion = new Suggestion()
            {
                UserId = _claimService.GetCurrentUserId,
                IsDelete = false,

            };
            if (pointTemp.QuizTypeId == 1)
            {
                suggestion.MajorId = 1;
                suggestion.suggestionContent = "You are suitable for IT";
            }
            else if (pointTemp.QuizTypeId == 2)
            {
                suggestion.MajorId = 2;
                suggestion.suggestionContent = "You are suitable for Graphic Design";
            }
            else if (pointTemp.QuizTypeId == 3)
            {
                suggestion.MajorId = 3;
                suggestion.suggestionContent = "You are suitable for Digital Marketing";
            }
            await _unitOfWork.SuggestionRepository.AddAsync(suggestion);
            await _unitOfWork.SaveChangeAsync();
            Major majorDetail = await _unitOfWork.MajorRepository.GetMajorDetail(suggestion.MajorId.Value);
            User loginUser = await _unitOfWork.UserRepository.GetByIdAsync(_claimService.GetCurrentUserId);
            SuggestionViewModel suggestionView = new SuggestionViewModel()
            {
                Username=loginUser.UserName,
                MajorName = majorDetail.MajorName,
                SuggestionContent = suggestion.suggestionContent
            };
            return suggestionView;
        }

        public async Task<List<PointViewModel>> SeePoints()
        {
            List<CareerQuiz> listQuizzes = await _unitOfWork.CareerQuizRepository.GetAllAsync();
            int amountQuiz = listQuizzes.Count();
            return await _unitOfWork.UserResponse.CalculatePoint(_claimService.GetCurrentUserId, amountQuiz);
        }
    }
}
