using Api.Dtos.Dependent;
using Api.Models;
using Api.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly IDependentRepository _repository;
    private readonly IMapper _mapper;

    public DependentsController(IDependentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    // Get dependent by id
    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        // Retrieve dependent by id
        var dependent = await _repository.GetDependentByIdAsync(id);
        if (dependent == null)
        {
            return NotFound("Dependent not found.");
        }

        // Map dependent to DTO and return
        var dependentDto = _mapper.Map<GetDependentDto>(dependent);
        return Ok(new ApiResponse<GetDependentDto> { Data = dependentDto, Success = true });
    }

    // Get all dependents
    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        // Retrieve all dependents
        var dependents = await _repository.GetAllDependentsAsync();
        // Map dependents to DTOs and return
        var dependentDtos = _mapper.Map<List<GetDependentDto>>(dependents);
        return Ok(new ApiResponse<List<GetDependentDto>> { Data = dependentDtos, Success = true });
    }
}
