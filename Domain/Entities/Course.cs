using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Course:BaseEntity
    {
        public string CourseName { get; set; }
        public string CourseDescription { get;set; }
        public int? MajorId { get; set; }
        public Major Major { get; set; }
    }
}
