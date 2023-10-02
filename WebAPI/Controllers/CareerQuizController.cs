using Application.InterfaceService;
using Application.ViewModel.QuizModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [EnableCors]
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
        [HttpDelete]
        public async Task<IActionResult> DeleteQuiz(Guid id)
        { 
           bool isDeleted= await _careerQuizService.DeleteQuiz(id);
            if (isDeleted)
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}
