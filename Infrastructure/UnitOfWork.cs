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
        private readonly IUserResponseRepository _userResponseRepository;
        private readonly ISuggestionRepository _suggestionRepository;
        private readonly IMajorRepository _majorRepository;
        public UnitOfWork(AppDbContext appDbContext, IUserRepository userRepository,
            ICareerQuizRepository careerQuizRepository, IQuizOptionRepository quizOptionRepository,
            IQuizTypeRepository quizTypeRepository, IUserResponseRepository userResponseRepository, 
            ISuggestionRepository suggestionRepository, IMajorRepository majorRepository)
        {
            _appDbContext = appDbContext;
            _userRepository = userRepository;
            _careerQuizRepository = careerQuizRepository;
            _quizOptionRepository = quizOptionRepository;
            _quizTypeRepository = quizTypeRepository;
            _userResponseRepository = userResponseRepository;
            _suggestionRepository = suggestionRepository;
            _majorRepository = majorRepository;
        }
        public IUserRepository UserRepository => _userRepository;

        public ICareerQuizRepository CareerQuizRepository => _careerQuizRepository;

        public IQuizOptionRepository QuizOptionRepository => _quizOptionRepository;

        public IQuizTypeRepository QuizTypeRepository => _quizTypeRepository;

        public IUserResponseRepository UserResponse => _userResponseRepository;

        public ISuggestionRepository SuggestionRepository => _suggestionRepository;

        public IMajorRepository MajorRepository => _majorRepository;

        public async Task<int> SaveChangeAsync()
        {
           return await _appDbContext.SaveChangesAsync();
        }
    }
}
