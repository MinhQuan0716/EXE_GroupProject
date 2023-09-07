using Application;
using Application.InterfaceRepository;
using Infrastructure.Repository;
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
        private readonly ICareerQuizRepository _careerQuizRepository;
        private readonly IQuizOptionRepository _quizOptionRepository;
        private readonly IQuizTypeRepository _quizTypeRepository;
        public UnitOfWork(AppDbContext appDbContext, IUserRepository userRepository,
            ICareerQuizRepository careerQuizRepository, IQuizOptionRepository quizOptionRepository, IQuizTypeRepository quizTypeRepository)
        {
            _appDbContext = appDbContext;
            _userRepository = userRepository;
            _careerQuizRepository = careerQuizRepository;
            _quizOptionRepository = quizOptionRepository;
            _quizTypeRepository = quizTypeRepository;
        }
        public IUserRepository UserRepository => _userRepository;

        public ICareerQuizRepository CareerQuizRepository => _careerQuizRepository;

        public IQuizOptionRepository QuizOptionRepository => _quizOptionRepository;

        public IQuizTypeRepository QuizTypeRepository => _quizTypeRepository;

        public async Task<int> SaveChangeAsync()
        {
           return await _appDbContext.SaveChangesAsync();
        }
    }
}
