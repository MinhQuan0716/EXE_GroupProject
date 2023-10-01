using Application.InterfaceService;
using Application.ViewModel.QuizModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    public class CareerQuizController : MainController
    {
        private readonly ICareerQuizService _careerQuizService;
        public CareerQuizController(ICareerQuizService careerQuizService)
        {
            _careerQuizService = careerQuizService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCareerQuiz(CreateCareerQuizModel createCareerQuizModel)
        {
            bool isCreated=await _careerQuizService.CreateQuiz(createCareerQuizModel);
            if (!isCreated) 
            { 
            return BadRequest();
            }
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllQuizzes()
        {
            List<ViewCareerQuizModel> quizList = await _careerQuizService.GetAllQuiz();
            if (quizList != null)
            {
                return Ok(quizList);
            }
            return BadRequest();
        }
    }
}
