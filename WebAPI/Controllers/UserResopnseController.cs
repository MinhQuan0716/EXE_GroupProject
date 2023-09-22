using Application.InterfaceService;
using Application.ViewModel.QuizModel;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
   
    public class UserResopnseController : MainController
    {
       private IUserResopnseService _userResopnseService;
        public UserResopnseController(IUserResopnseService userResopnseService)
        {
            _userResopnseService = userResopnseService;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DoingQuiz(List<CreateResponseViewModel> createResponseViewModels)
        {
         SuggestionViewModel isCreate = await _userResopnseService.DoingQuiz(createResponseViewModels);
            if (isCreate!=null) 
            {
                return new ObjectResult(isCreate)
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            return BadRequest();
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SeePoint()
        {
            List<PointViewModel> points = await _userResopnseService.SeePoints();
            return Ok(points);
        }
    }
}
