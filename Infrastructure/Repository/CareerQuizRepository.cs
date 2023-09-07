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

        public async Task<CareerQuiz> GetlastSavedQuiz()
        {
            return await _appDbContext.CareerQuizzes.OrderByDescending(x => x.CreationDate).FirstAsync();
        }
    }
}
