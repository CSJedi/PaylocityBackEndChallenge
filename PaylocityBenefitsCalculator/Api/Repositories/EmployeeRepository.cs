﻿using Api.Data;
using Api.Models;
using Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Get all employees asynchronously, including their dependents
    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return await _context.Employees.Include(e => e.Dependents).ToListAsync();
    }

    // Get employee by id asynchronously, including their dependents
    public async Task<Employee> GetEmployeeByIdAsync(int id)
    {
        return await _context.Employees.Include(e => e.Dependents).FirstOrDefaultAsync(e => e.Id == id);
    }
}
