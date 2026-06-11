using OpteamixEmployeeManagementSystem.Domain.Entities;

namespace OpteamixEmployeeManagementSystem.Domain.DTOs
{
    public static class EmployeeConversions
    {
        // Entity -> DTO
        public static EmployeeDto FromEntity(Employee employee)
        {
            return new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,

                DepartmentId = employee.DepartmentId,

                DepartmentName =
                    employee.Department?.Name ?? string.Empty,

                Designation = employee.Designation,

                JoiningDate = employee.JoiningDate
            };
        }

        // CreateEmployeeDto -> Entity
        public static Employee ToEntity(CreateEmployeeDto dto)
        {
            return new Employee
            {
                Name = dto.Name,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,

                DepartmentId = dto.DepartmentId,

                Designation = dto.Designation,

                JoiningDate = dto.JoiningDate
            };
        }

        // UpdateEmployeeDto -> Entity
        public static Employee ToEntity(UpdateEmployeeDto dto)
        {
            return new Employee
            {
                EmployeeId = dto.EmployeeId,

                Name = dto.Name,

                Email = dto.Email,

                PhoneNumber = dto.PhoneNumber,

                DepartmentId = dto.DepartmentId,

                Designation = dto.Designation,

                JoiningDate = dto.JoiningDate
            };
        }
    }
}