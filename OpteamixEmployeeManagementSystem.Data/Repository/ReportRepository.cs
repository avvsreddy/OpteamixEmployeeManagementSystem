using Microsoft.EntityFrameworkCore;
using OpteamixEmployeeManagementSystem.Domain.DTOs;
using OpteamixEmployeeManagementSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpteamixEmployeeManagementSystem.Data.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly EmployeeDbContext _context;

        public ReportRepository(EmployeeDbContext context)
        {
            _context = context;
        }
        public Task<int> GetCompletedTasksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetPendingTasksAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetTotalEmployeesAsync()
        {
            return await _context.Employees.CountAsync();
        }

        public async Task<int> GetTotalProjectsAsync()
        {
            return await _context.Projects.CountAsync();
        }

        public Task<int> GetTotalTasksAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ProjectStatusSummaryDto> GetProjectStatusSummaryAsync()
        {
            return new ProjectStatusSummaryDto
            {
                ActiveProjects = await _context.Projects.CountAsync(p => p.Status == "Active"),
                InactiveProjects = await _context.Projects.CountAsync(p => p.Status == "Inactive"),
                CompletedProjects = await _context.Projects.CountAsync(p => p.Status == "Completed")
            };
        }
    }
}
