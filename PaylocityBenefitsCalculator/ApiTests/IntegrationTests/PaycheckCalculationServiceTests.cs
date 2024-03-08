using Api.Models;
using Api.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ApiTests.IntegrationTests;

public class PaycheckCalculationServiceTests
{
    [Fact]
    public async Task CalculatePaycheckAsync_ShouldReturnCorrectPaycheckForLeBronJames()
    {
        // Arrange
        var employee = new Employee
        {
            Salary = 75420.99m, // Below the salary threshold
            Dependents = new List<Dependent>()
        };

        var service = new PaycheckCalculationService();

        // Act
        var paycheck = await service.CalculatePaycheckAsync(employee);

        // Assert
        Assert.Equal(2439.26m, paycheck); // Assuming the expected paycheck value
    }

    [Fact]
    public async Task CalculatePaycheckAsync_ShouldReturnCorrectPaycheckForJaMorant()
    {
        // Arrange
        var employee = new Employee
        {
            Id = 2,
            FirstName = "Ja",
            LastName = "Morant",
            Salary = 92365.22m, // Above the salary threshold
            DateOfBirth = new DateTime(1999, 8, 10),
            Dependents = new List<Dependent>
            {
                new Dependent 
                {
                    EmployeeId = 2,
                    Id = 1,
                    FirstName = "Spouse",
                    LastName = "Morant",
                    Relationship = Relationship.Spouse,
                    DateOfBirth = new DateTime(1998, 3, 3)
                },
                new Dependent
                {
                    EmployeeId = 2,
                    Id = 2,
                    FirstName = "Child1",
                    LastName = "Morant",
                    Relationship = Relationship.Child,
                    DateOfBirth = new DateTime(2020, 6, 23)
                },
                new Dependent
                {
                    EmployeeId = 2,
                    Id = 3,
                    FirstName = "Child2",
                    LastName = "Morant",
                    Relationship = Relationship.Child,
                    DateOfBirth = new DateTime(2021, 5, 18)
                },
            }
        };

        var service = new PaycheckCalculationService();

        // Act
        var paycheck = await service.CalculatePaycheckAsync(employee);

        // Assert
        Assert.Equal(2189.15m, paycheck); // Assuming the expected paycheck value
    }

    [Fact]
    public async Task CalculatePaycheckAsync_ShouldReturnCorrectPaycheckForMichaelJordan()
    {
        // Arrange
        var employee = new Employee
        {
            Id = 3,
            FirstName = "Michael",
            LastName = "Jordan",
            Salary = 143211.12m,// Above the salary threshold
            DateOfBirth = new DateTime(1963, 2, 17),
            Dependents = new List<Dependent>
            {
                new Dependent
                {
                    EmployeeId = 3,
                    Id = 4,
                    FirstName = "DP",
                    LastName = "Jordan",
                    Relationship = Relationship.DomesticPartner,
                    DateOfBirth = new DateTime(1974, 1, 2)
                } // Older than the age threshold
            }
        };

        var service = new PaycheckCalculationService();

        // Act
        var paycheck = await service.CalculatePaycheckAsync(employee);

        // Assert
        Assert.Equal(4567.18m, paycheck); // Assuming the expected paycheck value
    }
}
