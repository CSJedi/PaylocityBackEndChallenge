# Paylocity Back-End Challenge

## Description

This project is a back-end solution for a hypothetical payroll system, implementing features such as calculating employee paychecks based on various factors like salary, dependents, and benefits costs.

## Technologies Used

- C#
- ASP.NET Core
- Entity Framework Core
- SQLite
- AutoMapper
- Swagger

## Setup

1. Clone the repository.
2. Open the project in Visual Studio or your preferred IDE.
3. Make sure you have the necessary dependencies installed.
4. Update the `appsettings.json` file with your database connection string if needed.
5. Run the following commands in the Package Manager Console to update the database:
   ```bash
   Update-Database
   ```
6. Build the project.

## How to Run

1. Start the project.
2. Use a tool like Postman or Swagger UI to interact with the API endpoints.

## API Endpoints

- `/api/v1/employees`: Get all employees or create a new employee.
- `/api/v1/employees/{id}`: Get, update, or delete an employee by ID.
- `/api/v1/dependents`: Get all dependents or create a new dependent.
- `/api/v1/dependents/{id}`: Get, update, or delete a dependent by ID.
- `/api/v1/paychecks`: Get all paychecks for employees.

## How to Contribute

1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Make your changes.
4. Commit your changes (`git commit -am 'Add new feature'`).
5. Push to the branch (`git push origin feature-branch`).
6. Create a new Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.