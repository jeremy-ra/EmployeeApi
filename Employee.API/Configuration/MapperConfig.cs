using AutoMapper;
using Employee.Entity.Entities;
using Employee.Service.DataTransferObjects;

namespace Employee.API.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            CreateMap<EmployeeInformation, GetEmployeeInformationDto>().ReverseMap();
            CreateMap<EmployeeInformation, CreateEmployeeInformationDto>().ReverseMap();
            CreateMap<EmployeeInformation, UpdateEmployeeInformationDto>().ReverseMap();
        }
    }
}
