﻿using Application.InterfaceRepository;
using Application.InterfaceService;
using Application.ViewModel;
using Application.ViewModel.UserModel;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;
        public UserRepository(AppDbContext dbContext, ICurrentTime timeService, IClaimService claimsService) : base(dbContext, timeService, claimsService)
        {
            _dbContext = dbContext;
            _claimService = claimsService;
            _currentTime = timeService;
        }
        public async Task<bool> CheckMailExisted(string email)
        {
            return await _dbContext.Users.AnyAsync(x=>x.Email.Equals(email)); 
        }

        public async Task<User> FindUserByEmail(string email)
        {
            return await _dbContext.Users.Include(x=>x.Role).FirstAsync(x => x.Email.Equals(email));
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _dbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(e => e.Email == email);
            if (user == null)
            {
                throw new Exception("UserName is not exist!");
            }
            return user;
        }

       public async Task<List<UserInformationViewModel>> GetAllUsers()
        {
            var user= await _dbContext.Users.Where(x=>x.IsDelete==false)
                                            .Select(x=>new UserInformationViewModel
                                            {
                                                UserId=x.Id,
                                                BirthDay= x.BirthDay,
                                                Email= x.Email,
                                                RoleName=x.Role.RoleName,
                                                UserName=x.UserName,
                                            }).ToListAsync();
            return user;
        }
    }
}
