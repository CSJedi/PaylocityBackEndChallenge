using Api.Models;
using Api.Services.Interfaces;

namespace Api.Services;

public class PaycheckCalculationService : IPaycheckCalculationService
{
    // Constants for calculation
    private const int AgeThreshold = 50;
    private const int MonthPerYear = 12;
    private const int PaychecksPerYear = 26;
    private const decimal BaseCostPerMonth = 1000m;
    private const decimal SalaryThreshold = 80000.00m;
    private const decimal DependentCostPerMonth = 600m;
    private const decimal AdditionalCostPercentage = 0.02m;
    private const decimal AgeBasedAdditionalCostPerMonth = 200m;

    // Pre-calculated dependent costs per year
    private static readonly decimal DependentCostPerYear = DependentCostPerMonth * MonthPerYear;
    private static readonly decimal AgeBasedAdditionalCostPerYear = AgeBasedAdditionalCostPerMonth * MonthPerYear;

    // Calculate the paycheck asynchronously
    public async Task<decimal> CalculatePaycheckAsync(Employee employee)
    {
        // Calculate dependent costs
        decimal dependentsCost = await Task.Run(() => GetDependentsWithRelationship(employee.Dependents).Count * DependentCostPerYear);
        decimal dependentsOverAgeThresholdCost = await Task.Run(() => GetDependentsWithRelationship(GetDependentsOverAgeThreshold(employee.Dependents)).Count * AgeBasedAdditionalCostPerYear);

        // Calculate additional cost for salary over $80k
        decimal over80kCost = (employee.Salary > SalaryThreshold) ? employee.Salary * AdditionalCostPercentage : 0;

        // Calculate the yearly salary after deducting benefits
        decimal yearlySalaryAfterBenefits = employee.Salary - (BaseCostPerMonth * MonthPerYear) - dependentsCost - dependentsOverAgeThresholdCost - over80kCost;

        // Calculate the bi-weekly paycheck amount
        decimal salary = Math.Round(yearlySalaryAfterBenefits / PaychecksPerYear, 2, MidpointRounding.ToZero);

        return salary;
    }

    // Helper method to filter dependents with relationships
    private static List<Dependent> GetDependentsWithRelationship(ICollection<Dependent> dependents)
    {
        return dependents.Where(d => d.Relationship != Relationship.None).ToList();
    }

    // Helper method to filter dependents over age threshold
    private static List<Dependent> GetDependentsOverAgeThreshold(ICollection<Dependent> dependents)
    {
        return dependents.Where(d => d.DateOfBirth < DateTime.Now.AddYears(-AgeThreshold)).ToList();
    }
}
