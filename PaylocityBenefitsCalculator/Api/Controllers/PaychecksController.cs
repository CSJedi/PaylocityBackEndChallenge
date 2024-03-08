using Api.Dtos.Paycheck;
using Api.Models;
using Api.Repositories.Interfaces;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PaychecksController : ControllerBase
{
    private readonly IPaycheckCalculationService _paycheckCalculationService;
    private readonly IEmployeeRepository _employeeRepository;

    public PaychecksController(IEmployeeRepository employeeRepository, IPaycheckCalculationService paycheckCalculationService)
    {
        _employeeRepository = employeeRepository;
        _paycheckCalculationService = paycheckCalculationService;
    }

    // Get paychecks for all employees
    [SwaggerOperation(Summary = "Get paychecks for all employees")]
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<GetPaycheckDto>>>> GetAll()
    {
        // Retrieve all employees
        var employees = await _employeeRepository.GetAllEmployeesAsync();
        var paycheckDtos = new List<GetPaycheckDto>();
        foreach (var employee in employees)
        {
            // Calculate paycheck for each employee
            var salary = await _paycheckCalculationService.CalculatePaycheckAsync(employee);
            paycheckDtos.Add(new GetPaycheckDto
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = salary
            });
        }

        return Ok(new ApiResponse<List<GetPaycheckDto>> { Data = paycheckDtos, Success = true });
    }

    // Get paychecks by employeeId
    [SwaggerOperation(Summary = "Get paychecks by employeeId")]
    [HttpGet("{employeeId}")]
    public async Task<ActionResult<ApiResponse<GetPaycheckDto>>> Get(int employeeId)
    {
        // Retrieve employee by employeeId
        var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);

        if (employee == null)
        {
            return NotFound("Employee not found.");
        }

        // Calculate paycheck for the employee
        var salary = await _paycheckCalculationService.CalculatePaycheckAsync(employee);
        var paycheck = new GetPaycheckDto
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Salary = salary
        };

        return Ok(new ApiResponse<GetPaycheckDto> { Data = paycheck, Success = true });
    }
}
