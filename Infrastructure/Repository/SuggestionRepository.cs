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
    public class SuggestionRepository : GenericRepository<Suggestion>, ISuggestionRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly ICurrentTime _currentTime;
        private readonly IClaimService _claimService;
        public SuggestionRepository(AppDbContext dbContext, ICurrentTime timeService, IClaimService claimsService) : base(dbContext, timeService, claimsService)
        {
            _dbContext = dbContext;
            _currentTime=timeService; 
            _claimService= claimsService;
        }
    }
}
