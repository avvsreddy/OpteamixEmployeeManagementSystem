using OpteamixEmployeeManagementSystem.Domain.Entities;
using OpteamixEmployeeManagementSystem.Domain.Repositories;

namespace OpteamixEmployeeManagementSystem.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private static List<Employee> employees =
        [
            new Employee
            {
                EmployeeId = 1,
                Name = "John",
                Email = "john@gmail.com",
                PhoneNumber = "9876543210",
                Department = "IT",
                Designation = "Developer",
                JoiningDate = DateTime.Now
            }
        ];

        public List<Employee> GetEmployees()
        {
            return employees;
        }

        public Employee? GetEmployeeById(int employeeId)
        {
            return employees.FirstOrDefault
            (
                e => e.EmployeeId == employeeId
            );
        }

        public Employee AddEmployee(Employee employee)
        {
            employee.EmployeeId =
                employees.Max(e => e.EmployeeId) + 1;

            employees.Add(employee);

            return employee;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            var existingEmployee =
                employees.FirstOrDefault
                (
                    e => e.EmployeeId == employee.EmployeeId
                );

            if (existingEmployee != null)
            {
                existingEmployee.Name = employee.Name;
                existingEmployee.Email = employee.Email;
                existingEmployee.PhoneNumber = employee.PhoneNumber;
                existingEmployee.Department = employee.Department;
                existingEmployee.Designation = employee.Designation;
                existingEmployee.JoiningDate = employee.JoiningDate;
            }

            return employee;
        }

        public bool DeleteEmployee(int employeeId)
        {
            var employee =
                employees.FirstOrDefault
                (
                    e => e.EmployeeId == employeeId
                );

            if (employee == null)
                return false;

            employees.Remove(employee);

            return true;
        }
    }
}
