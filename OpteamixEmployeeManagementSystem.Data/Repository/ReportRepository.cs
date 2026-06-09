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
                .AsNoTracking()
                .CountAsync(e => e.Status == TaskItemStatus.Completed);
        }

        public async Task<int> GetPendingTasksAsync()
        {
            return await _context.Tasks
                .AsNoTracking()
                .CountAsync(e => e.Status != TaskItemStatus.Completed);
        }

        public async Task<int> GetTotalEmployeesAsync()
        {
            return await _context.Employees
                .AsNoTracking()
                .CountAsync();
        }

        public async Task<int> GetTotalProjectsAsync()
        {
            return await _context.Projects
                .AsNoTracking()
                .CountAsync();
        }

        public async Task<int> GetTotalTasksAsync()
        {
            return await _context.Tasks
                .AsNoTracking()
                .CountAsync();
        }

        public async Task<ProjectStatusSummaryDto> GetProjectStatusSummaryAsync()
        {
            var projects = await _context.Projects
                .AsNoTracking()
                .GroupBy(p => p.Status)
                .Select(g => new
                {
                    Status = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            return new ProjectStatusSummaryDto
            {
                ActiveProjects =
                    projects.FirstOrDefault(
                        p => p.Status == "Active")?.Count ?? 0,

                InactiveProjects =
                    projects.FirstOrDefault(
                        p => p.Status == "Inactive")?.Count ?? 0,

                CompletedProjects =
                    projects.FirstOrDefault(
                        p => p.Status == "Completed")?.Count ?? 0
            };
        }
    }
}