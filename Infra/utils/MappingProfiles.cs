using AutoMapper;
using core.Dto;
using core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infra.utils
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<user, userDto>().ReverseMap();
            CreateMap<Product, productDto>().ReverseMap();
        }
    }
}
