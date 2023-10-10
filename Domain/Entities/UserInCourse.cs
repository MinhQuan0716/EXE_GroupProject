using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class UserInCourse:BaseEntity
    {
        public Guid? UserId { get; set; }
        public User User { get; set; }
        public Guid? CourseId { get; set; }
        public Course Course { get; set;}
        public string UserStatus { get; set; }
    }
}
