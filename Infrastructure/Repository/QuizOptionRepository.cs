using Application.InterfaceRepository;
using Application.InterfaceService;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class QuizOptionRepository : GenericRepository<QuizOption>, IQuizOptionRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICurrentTime _currentTime;
        private IClaimService _claimService;
        public QuizOptionRepository(AppDbContext dbContext, ICurrentTime timeService, IClaimService claimsService) : base(dbContext, timeService, claimsService)
        {
            _appDbContext = dbContext;
            _currentTime = timeService;
            _claimService = claimsService;
        }

        public async Task<Guid> GetOptionByInterestLevel(int option,Guid careerQuizId)
        {
            QuizOption findOption= await _appDbContext.QuizOptions.Include(x=>x.CareerQuiz).Where(x=>x.InterestLevel==option&&x.CareerQuizId.Equals(careerQuizId)).SingleAsync();
            return findOption.Id;
        }

        public async Task<List<QuizOption>> GetOptionsByQuizId(Guid quizId)
        {
            return await _appDbContext.QuizOptions.Where(x => x.CareerQuizId.Equals(quizId)).ToListAsync();
        }

        public async Task RemoveRangeOption(Guid quizId)
        {
            List<QuizOption> listOptions = await GetOptionsByQuizId( quizId);
             _appDbContext.QuizOptions.RemoveRange(listOptions);
        }

    }
}
