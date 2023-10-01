using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.ViewModel.QuizModel
{
    public class CreateCareerQuizModel
    {
        public string QuizText { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public QuizTypeEnum QuizType { get; set; }
    }
}
