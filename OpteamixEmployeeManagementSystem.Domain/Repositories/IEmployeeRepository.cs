using OpteamixEmployeeManagementSystem.Domain.Entities;

namespace OpteamixEmployeeManagementSystem.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        List<Employee> GetEmployees();

        Employee? GetEmployeeById(int employeeId);

        Employee AddEmployee(Employee employee);

        Employee UpdateEmployee(Employee employee);

        bool DeleteEmployee(int employeeId);
    }
}