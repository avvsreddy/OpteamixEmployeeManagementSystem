using OpteamixEmployeeManagementSystem.Domain.Entities;

namespace OpteamixEmployeeManagementSystem.Domain.DTOs
{
    public static class EmployeeConversions
    {
        public static EmployeeDto FromEntity(Employee employee)
        {
            return new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                EmployeeCode = employee.EmployeeCode,

                Name = employee.Name,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,

                DepartmentId = employee.DepartmentId,

                DepartmentName =
                    employee.Department?.Name ?? string.Empty,

                Designation = employee.Designation,

                JoiningDate = employee.JoiningDate,

                Salary = employee.Salary
            };
        }

        public static Employee ToEntity(CreateEmployeeDto dto)
        {
            return new Employee
            {
                EmployeeCode = dto.EmployeeCode,

                Name = dto.Name,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,

                DepartmentId = dto.DepartmentId,

                Designation = dto.Designation,

                JoiningDate = dto.JoiningDate,

                Salary = dto.Salary
            };
        }

        public static Employee ToEntity(UpdateEmployeeDto dto)
        {
            return new Employee
            {
                EmployeeId = dto.EmployeeId,

                EmployeeCode = dto.EmployeeCode,

                Name = dto.Name,

                Email = dto.Email,

                PhoneNumber = dto.PhoneNumber,

                DepartmentId = dto.DepartmentId,

                Designation = dto.Designation,

                JoiningDate = dto.JoiningDate,

                Salary = dto.Salary
            };
        }
    }
}