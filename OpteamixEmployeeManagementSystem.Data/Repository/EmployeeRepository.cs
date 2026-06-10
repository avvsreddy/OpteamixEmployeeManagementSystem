using Microsoft.EntityFrameworkCore;
using OpteamixEmployeeManagementSystem.Domain.Entities;
using OpteamixEmployeeManagementSystem.Domain.Repositories;


namespace OpteamixEmployeeManagementSystem.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _context;

        public EmployeeRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int employeeId)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(
                    e => e.EmployeeId == employeeId);
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);

            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee?> UpdateEmployeeAsync(Employee employee)
        {
            var existingEmployee =
                await _context.Employees
                .FirstOrDefaultAsync(
                    e => e.EmployeeId == employee.EmployeeId);

            if (existingEmployee == null)
            {
                return null;
            }

            existingEmployee.Name = employee.Name;
            existingEmployee.Email = employee.Email;
            existingEmployee.PhoneNumber = employee.PhoneNumber;
            existingEmployee.DepartmentId = employee.DepartmentId;
            existingEmployee.Designation = employee.Designation;
            existingEmployee.JoiningDate = employee.JoiningDate;

            await _context.SaveChangesAsync();

            return existingEmployee;
        }

        public async Task<bool> DeleteEmployeeAsync(int employeeId)
        {
            var employee =
                await _context.Employees
                .FirstOrDefaultAsync(
                    e => e.EmployeeId == employeeId);

            if (employee == null)
            {
                return false;
            }

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Employee>> SearchEmployeesAsync(
    string keyword)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Where(e =>
                    e.Name.Contains(keyword) ||
                    e.Email.Contains(keyword) ||
                    e.Designation.Contains(keyword) ||
                    (e.Department != null &&
                     e.Department.Name.Contains(keyword)))
                .ToListAsync();
        }
    }
}