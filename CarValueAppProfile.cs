using CarValueApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarValueApi.Dto;

namespace CarValueApi.Profiles
{
    public class CarValueAppProfile : Profile
    {
        public CarValueAppProfile()
        {
            //Source -> Target
            CreateMap<Command, CarValueAppReadDto>();
            CreateMap<CarValueAppCreateDto, Command>();
            CreateMap<signUpModel, signUpDTO>();
        }


    }
}
