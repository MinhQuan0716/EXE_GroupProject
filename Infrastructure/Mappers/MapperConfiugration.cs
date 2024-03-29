﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModel.UserModel;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Mappers
{
    public  class MapperConfiugration:Profile
    {
        public MapperConfiugration()
        {
            CreateMap<User, LoginWithEmailViewModel>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();

            CreateMap<User, UserViewModel>()
                .ForMember(desc => desc._Id, src => src.MapFrom(u => u.Id))
                .ReverseMap();

            CreateMap<ResetPasswordDTO, User>()
                .ForMember(rp => rp.Password, opt => opt.MapFrom(src => src.NewPassword)).ReverseMap();
        }
    }

}
