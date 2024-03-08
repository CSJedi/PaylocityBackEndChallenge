using AutoMapper;
using Api.Dtos.Dependent;
using Api.Models;
using Api.Dtos.Employee;

namespace Api.Profiles;

public class DependentProfile : Profile
{
    public DependentProfile()
    {
        CreateMap<Dependent, GetDependentDto>();
        CreateMap<Employee, GetEmployeeDto>();
    }
}