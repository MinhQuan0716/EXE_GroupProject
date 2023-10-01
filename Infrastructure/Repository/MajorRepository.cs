using Application.InterfaceRepository;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public  class MajorRepository:IMajorRepository
    {
        private AppDbContext _appDbContext;
        public MajorRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Major> GetMajorDetail(int majorId)
        {
           return await _appDbContext.Major.FindAsync(majorId);
        }
    }
}
