using Api.Models;

namespace Api.Repositories.Interfaces;

public interface IDependentRepository
{
    Task<IEnumerable<Dependent>> GetAllDependentsAsync();
    Task<Dependent> GetDependentByIdAsync(int id);
}