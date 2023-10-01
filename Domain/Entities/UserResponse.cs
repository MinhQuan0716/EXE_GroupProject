using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserResponse : BaseEntity
    {
        public Guid? UserId { get; set; }
        public User User { get; set; }
        public Guid? SelectedOptionId { get; set; }
        public QuizOption SelectOption { get; set; }
    }
}
