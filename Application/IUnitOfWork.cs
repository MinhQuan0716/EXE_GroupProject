using Application.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public  interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public ICareerQuizRepository CareerQuizRepository { get; }
        public IQuizOptionRepository QuizOptionRepository { get; }
        public IQuizTypeRepository QuizTypeRepository { get; }
        public Task<int> SaveChangeAsync();
    }
}
