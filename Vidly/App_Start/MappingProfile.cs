using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<MembershipType, MembershipTypeDto>();

            Mapper.CreateMap<CustomerDto, Customer>();
            Mapper.CreateMap<MovieDto, Movie>();
            Mapper.CreateMap<MembershipTypeDto, MembershipType>();

            //Mapper.CreateMap<CustomerDto, Customer>()
            //    .ForMember(c => c.Id, opt => opt.Ignore());
            //Mapper.CreateMap<MovieDto, Movie>()
            //    .ForMember(m => m.Id, opt => opt.Ignore());

        }
    }

}
