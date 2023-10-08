using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModel;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Mappers
{
    public  class MapperConfiugration:Profile
    {
        public MapperConfiugration()
        {
            CreateMap<UserViewModel,User>().ReverseMap();
            CreateMap<LoginWithEmailViewModel,User>().ReverseMap();
        }
    }

}
