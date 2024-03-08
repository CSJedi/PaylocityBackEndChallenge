using Api.Models;
using Api.Services.Interfaces;

namespace Api.Services
{
    public class PaycheckCalculationService : IPaycheckCalculationService
    {
        private const int AgeThreshold = 50;
        private const int MonthPerYear = 12;
        private const int PaychecksPerYear = 26;
        private const decimal BaseCostPerMonth = 1000m;
        private const decimal SalaryThreshold = 80000.00m;
        private const decimal DependentCostPerMonth = 600m;
        private const decimal AdditionalCostPercentage = 0.02m;
        private const decimal AgeBasedAdditionalCostPerMonth = 200m;

        private static readonly decimal DependentCostPerYear = DependentCostPerMonth * MonthPerYear;
        private static readonly decimal AgeBasedAdditionalCostPerYear = AgeBasedAdditionalCostPerMonth * MonthPerYear;

        public async Task<decimal> CalculatePaycheckAsync(Employee employee)
        {
            decimal dependentsCost = await Task.Run(() => GetDependentsWithRelationship(employee.Dependents).Count * DependentCostPerYear);
            decimal dependentsOverAgeThresholdCost = await Task.Run(() => GetDependentsWithRelationship(GetDependentsOverAgeThreshold(employee.Dependents)).Count * AgeBasedAdditionalCostPerYear);
            
            decimal over80kCost = (employee.Salary > SalaryThreshold) ? employee.Salary * AdditionalCostPercentage : 0;
            decimal yearlySalaryAfterBenefits = employee.Salary - (BaseCostPerMonth * MonthPerYear) - dependentsCost - dependentsOverAgeThresholdCost - over80kCost;
            decimal salary = Math.Round(yearlySalaryAfterBenefits / PaychecksPerYear, 2, MidpointRounding.ToZero);

            return salary;
        }

        private static List<Dependent> GetDependentsWithRelationship(ICollection<Dependent> dependents)
        {
            return dependents.Where(d => d.Relationship != Relationship.None).ToList();
        }

        private static List<Dependent> GetDependentsOverAgeThreshold(ICollection<Dependent> dependents)
        {
            return dependents.Where(d => d.DateOfBirth < DateTime.Now.AddYears(-AgeThreshold)).ToList();
        }
    }
}
