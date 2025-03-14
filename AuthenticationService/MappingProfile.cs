﻿using AuthenticationService.Models;
using AuthenticationService.ViewModel;
using AutoMapper;

namespace AuthenticationService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>()
                .ConstructUsing(v=>new UserViewModel(v));
        }
    }
}
