using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Suggestion:BaseEntity
    {
        public Guid? UserId { get; set; }
        public User User { get; set; }
        public int? MajorId { get; set; }
        public Major Major { get; set; }
    }
}
