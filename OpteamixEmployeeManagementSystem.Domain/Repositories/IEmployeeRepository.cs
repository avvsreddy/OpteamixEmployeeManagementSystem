using OpteamixEmployeeManagementSystem.Domain.Entities;

namespace OpteamixEmployeeManagementSystem.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployeesAsync();

        Task<Employee?> GetEmployeeByIdAsync(int employeeId);

        Task<Employee> AddEmployeeAsync(Employee employee);

        Task<Employee?> UpdateEmployeeAsync(Employee employee);

        Task<bool> DeleteEmployeeAsync(int employeeId);

        Task<List<Employee>> SearchEmployeesAsync(string keyword);
    }
}