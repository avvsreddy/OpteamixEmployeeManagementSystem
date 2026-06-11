using Microsoft.EntityFrameworkCore;
using OpteamixEmployeeManagementSystem.Domain.Entities;
using OpteamixEmployeeManagementSystem.Domain.Repositories;

namespace OpteamixEmployeeManagementSystem.Data.Repository
{
    public class DepartmentRepository
        : IDepartmentRepository
    {
        private readonly EmployeeDbContext _context;

        public DepartmentRepository(
            EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<List<Department>>
            GetDepartmentsAsync()
        {
            return await _context.Department
                .ToListAsync();
        }

        public async Task<Department?>
            GetDepartmentByIdAsync(
                int departmentId)
        {
            return await _context.Department
                .FirstOrDefaultAsync(
                    d => d.DepartmentId ==
                         departmentId);
        }

        public async Task<Department>
            AddDepartmentAsync(
                Department department)
        {
            await _context.Department
                .AddAsync(department);

            await _context.SaveChangesAsync();

            return department;
        }

        public async Task<Department?>
            UpdateDepartmentAsync(
                Department department)
        {
            var existingDepartment =
                await _context.Department
                .FirstOrDefaultAsync(
                    d => d.DepartmentId ==
                         department.DepartmentId);

            if (existingDepartment == null)
            {
                return null;
            }

            existingDepartment.Name =
                department.Name;

            existingDepartment.Description =
                department.Description;

            await _context.SaveChangesAsync();

            return existingDepartment;
        }

        public async Task<bool>
            DeleteDepartmentAsync(
                int departmentId)
        {
            var department =
                await _context.Department
                .FirstOrDefaultAsync(
                    d => d.DepartmentId ==
                         departmentId);

            if (department == null)
            {
                return false;
            }

            _context.Department
                .Remove(department);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}