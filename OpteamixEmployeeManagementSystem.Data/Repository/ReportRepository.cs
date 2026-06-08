using Microsoft.EntityFrameworkCore;
using OpteamixEmployeeManagementSystem.Domain.DTOs;
using OpteamixEmployeeManagementSystem.Domain.Enums;
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
        public async Task<int> GetCompletedTasksAsync()
        {
            return await _context.Tasks
                .CountAsync(e => e.Status == TaskItemStatus.Completed);
        }

        public async Task<int> GetPendingTasksAsync()
        {
            return await _context.Tasks
                .CountAsync(e => e.Status != TaskItemStatus.Completed);
        }

        public async Task<int> GetTotalEmployeesAsync()
        {
            return await _context.Employees.CountAsync();
        }

        public async Task<int> GetTotalProjectsAsync()
        {
            return await _context.Projects.CountAsync();
        }

        public async Task<int> GetTotalTasksAsync()
        {
            return await _context.Tasks.CountAsync();
        }

        public async Task<ProjectStatusSummaryDto> GetProjectStatusSummaryAsync()
        {
            return new ProjectStatusSummaryDto
            {
            };
        }
    }
}
