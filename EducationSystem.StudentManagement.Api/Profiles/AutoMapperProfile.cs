using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Common.ValueObjects;
using EducationSystem.StudentManagement.Core;
using EducationSystem.StudentManagement.Dtos;

namespace EducationSystem.StudentManagement.Api.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, StudentDto>()
                .ForMember(x => x.FirstName, x => x.MapFrom(y => y.FullName.FirstName))
                .ForMember(x => x.LastName, x => x.MapFrom(y => y.FullName.LastName))
                .ForMember(x => x.MiddleName, x => x.MapFrom(y => y.FullName.MiddleName))
                .ForMember(x => x.Passport, x => x.MapFrom(y => y.Passport.Number))
                .ForMember(x => x.PhotoUrl, x => x.MapFrom(y => y.PhotoUrl.Url))
                .ReverseMap();

            CreateMap<Phone, PhoneDto>()
                .ReverseMap();
        }
    }
}
