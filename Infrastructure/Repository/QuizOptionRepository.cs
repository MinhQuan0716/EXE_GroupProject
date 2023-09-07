using Application.InterfaceRepository;
using Application.InterfaceService;
using Domain.Entities;
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
    }
}
