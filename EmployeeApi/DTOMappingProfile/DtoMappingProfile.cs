using AutoMapper;
using EmployeeApi.Models;
using EmployeeApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.DTOMappingProfile
{
    public class DtoMappingProfile:Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, EmployeeListDto>().ReverseMap();
        }
    }
}
