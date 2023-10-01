using Application.InterfaceRepository;
using Application.InterfaceService;
using Application.ViewModel.QuizModel;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserResponseRepository : GenericRepository<UserResponse>, IUserResponseRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly ICurrentTime _currentTime;
        private readonly IClaimService _claimService;
        public UserResponseRepository(AppDbContext dbContext, ICurrentTime timeService, IClaimService claimsService) : base(dbContext, timeService, claimsService)
        {
            _dbContext = dbContext;
            _currentTime = timeService;
            _claimService = claimsService;
        }

        public async Task DeleteResponse(Guid id)
        {
            var response = await _dbContext.UserResponses.Where(x => x.SelectedOptionId.Equals(id)).ToListAsync();
            _dbContext.UserResponses.RemoveRange(response);
        }
        public async Task<List<PointViewModel>> CalculatePoint(Guid userId,int questionAmount)
        {
            var pointList = await _dbContext.UserResponses.Include(x => x.SelectOption)
                                                      .ThenInclude(opt => opt.CareerQuiz)
                                                      .ThenInclude(quiz => quiz.QuizType)
                                                      .OrderByDescending(x=>x.CreationDate)
                                                      .Where(x=>x.UserId.Equals(userId))
                                                      .Select(x => new PointViewModel
                                                      {
                                                          QuizTypeId = x.SelectOption.CareerQuiz.TypeId,
                                                          TotalPoint = x.SelectOption.InterestLevel
                                                      }).Take(questionAmount)
                                                      .GroupBy(x => x.QuizTypeId)
                                                        .Select(group => new PointViewModel
                                                        {
                                                            QuizTypeId = group.Key,
                                                            TotalPoint = group.Sum(x => x.TotalPoint)
                                                        }).ToListAsync();
            return pointList;
        }

    }
}
