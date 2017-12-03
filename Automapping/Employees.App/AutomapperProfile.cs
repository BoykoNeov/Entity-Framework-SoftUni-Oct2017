namespace Employees.App
{
    using AutoMapper;
    using Employees.Models;
    using Employees.DtoModels;
    using System.Linq;

    class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<Employee, EmployeePersonalInfoDTO>();
            CreateMap<EmployeePersonalInfoDTO, Employee>();
            CreateMap<Employee, ManagerDto>()
             // test
             //   .ForMember(dto => dto.Test, opt => opt.MapFrom(src => src.Subordinates.Take(2).ToArray()));
            ;
        }
    }
}