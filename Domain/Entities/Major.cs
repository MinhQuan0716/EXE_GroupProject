using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Major
    {
        public int MajorId { get; set; }    
        public string MajorName { get; set;}
        public string MajorDescription { get; set;}
        public ICollection<Suggestion> Suggestions { get; set;}
    }
}
