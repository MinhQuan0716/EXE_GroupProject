using Application.ViewModel;
using Application.ViewModel.UserModel;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceRepository
{
    public  interface IUserRepository:IGenericRepository<User>
    {     
        public Task<bool> CheckMailExisted(string email);
        public Task<User>FindUserByEmail(string email);
        public Task<List<UserInformationViewModel>> GetAllUsers();
        public Task<User> GetUserByEmailAsync(string email);
    }
}
