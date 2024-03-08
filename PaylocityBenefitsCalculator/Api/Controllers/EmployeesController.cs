using Api.Dtos.Employee;
using Api.Models;
using Api.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeRepository _repository;
    private readonly IMapper _mapper;

    public EmployeesController(IEmployeeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    // Get employee by id
    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        // Retrieve employee by id
        var employee = await _repository.GetEmployeeByIdAsync(id);
        if (employee == null)
        {
            return NotFound("Employee not found.");
        }

        // Map employee to DTO and return
        var employeeDto = _mapper.Map<GetEmployeeDto>(employee);
        return Ok(new ApiResponse<GetEmployeeDto> { Data = employeeDto, Success = true });
    }


    // Get all employees
    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        // Retrieve all employees
        var employees = await _repository.GetAllEmployeesAsync();
        // Map employees to DTOs and return
        var employeeDtos = _mapper.Map<List<GetEmployeeDto>>(employees);
        return Ok(new ApiResponse<List<GetEmployeeDto>> { Data = employeeDtos, Success = true });
    }
}
