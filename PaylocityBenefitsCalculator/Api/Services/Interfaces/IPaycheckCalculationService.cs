using Api.Models;

namespace Api.Services.Interfaces;

public interface IPaycheckCalculationService
{
    Task<decimal> CalculatePaycheckAsync(Employee employee);
}
