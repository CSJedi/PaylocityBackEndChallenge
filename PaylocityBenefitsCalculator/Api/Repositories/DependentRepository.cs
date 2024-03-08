using Api.Data;
using Api.Models;
using Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class DependentRepository : IDependentRepository
{
    private readonly ApplicationDbContext _context;

    public DependentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Get all dependents asynchronously
    public async Task<IEnumerable<Dependent>> GetAllDependentsAsync()
    {
        return await _context.Dependents.ToListAsync();
    }

    // Get dependent by id asynchronously
    public async Task<Dependent> GetDependentByIdAsync(int id)
    {
        return await _context.Dependents.FindAsync(id);
    }
}
