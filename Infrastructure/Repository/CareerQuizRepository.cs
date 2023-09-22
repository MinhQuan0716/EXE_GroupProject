using Application.InterfaceRepository;
using Application.InterfaceService;
using Application.ViewModel.QuizModel;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class CareerQuizRepository : GenericRepository<CareerQuiz>,ICareerQuizRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICurrentTime _currentTime;
        private IClaimService _claimService;
        public CareerQuizRepository(AppDbContext dbContext, ICurrentTime timeService, IClaimService claimsService) : base(dbContext, timeService, claimsService)
        {
            _appDbContext= dbContext;
            _currentTime= timeService;
            _claimService= claimsService;
        }

        public async Task<List<ViewCareerQuizModel>> GetAllQuiz()
        {
          List<ViewCareerQuizModel> quizList = await _appDbContext.CareerQuizzes.Include(x=>x.QuizOptions)
                .Where(x=>x.IsDelete==false)
                .OrderBy(x=>x.CreationDate)
                .Select(
                  x => new ViewCareerQuizModel
               {
                   Question=x.QuizText,
                   Option= x.QuizOptions.OrderBy(x=>x.InterestLevel).Select(x=>x.InterestLevel).ToList(),
               }).ToListAsync();
            return quizList;
        }

        public async Task<CareerQuiz> GetlastSavedQuiz()
        {
            return await _appDbContext.CareerQuizzes.OrderByDescending(x => x.CreationDate).FirstAsync();
        }

        public async Task<Guid> GetQuizIdFromQuizText(string text)
        {
            CareerQuiz careerQuiz = await _appDbContext.CareerQuizzes.Where(x=>x.QuizText.Equals(text)).SingleAsync();
            return careerQuiz.Id;
        }

        public async Task RemoveQuiz(Guid id)
        {
         var careerQuiz=   await _appDbContext.CareerQuizzes.FindAsync(id);
             _appDbContext.Remove(careerQuiz);
        }
    }
}
