using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel
{
    public class UserViewModel
    {
        public string _Id { get; set; }
        public string UserName { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
