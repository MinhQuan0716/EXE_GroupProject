using Application;
using Application.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserRepository _userRepository;
        public UnitOfWork(AppDbContext appDbContext, IUserRepository userRepository)
        {
            _appDbContext = appDbContext;
            _userRepository = userRepository;
        }
        public IUserRepository UserRepository => _userRepository;

        public async Task<int> SaveChangeAsync()
        {
           return await _appDbContext.SaveChangesAsync();
        }
    }
}
