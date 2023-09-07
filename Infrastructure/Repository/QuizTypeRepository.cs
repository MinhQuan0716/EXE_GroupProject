using Application.InterfaceRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class QuizTypeRepository : IQuizTypeRepository
    {
        private readonly AppDbContext _appDbContext;
        public QuizTypeRepository(AppDbContext appDbContext) 
        { 
            _appDbContext = appDbContext;
        }
        public async Task<int> GetTypeIdByName(string name)
        {
          QuizType findType= await _appDbContext.QuizTypes.Where(x=>x.TypeName.Equals(name)).FirstAsync();
            int typeid=findType.TypeId;
            return typeid;
        }
    }
}
