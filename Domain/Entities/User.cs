using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class User:BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; } 
        public string? Password { get; set; }
        public DateTime BirthDay { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? ExpireTokenTime { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
        public ICollection<Suggestion> Suggestions { get; set; }
        public ICollection<UserResponse > UserResponses { get; set; }
    }
}
