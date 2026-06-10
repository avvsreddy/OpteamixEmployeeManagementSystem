using OpteamixEmployeeManagementSystem.Domain.Entities;

namespace OpteamixEmployeeManagementSystem.Domain.Repositories
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetDepartmentsAsync();

        Task<Department?> GetDepartmentByIdAsync(
            int departmentId);

        Task<Department> AddDepartmentAsync(
            Department department);

        Task<Department?> UpdateDepartmentAsync(
            Department department);

        Task<bool> DeleteDepartmentAsync(
            int departmentId);
    }
}