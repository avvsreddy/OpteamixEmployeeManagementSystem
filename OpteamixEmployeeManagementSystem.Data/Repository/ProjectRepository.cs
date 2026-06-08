using Microsoft.EntityFrameworkCore;
using OpteamixEmployeeManagementSystem.Domain.DTOs;
using OpteamixEmployeeManagementSystem.Domain.Entities;
using OpteamixEmployeeManagementSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpteamixEmployeeManagementSystem.Data.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly EmployeeDbContext _context;

        public ProjectRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _context.Projects
                .Include(p => p.Manager)
                .ToListAsync();
        }

        public async Task<Project?> GetProjectByIdAsync(int id)
        {
            return await _context.Projects
                .Include(p => p.Manager)
                .FirstOrDefaultAsync(p => p.ProjectId == id);
        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Project?> UpdateProjectAsync(int id, Project updatedProject)
        {
            var existing = await _context.Projects.FindAsync(id);

            if (existing == null)
                return null;

            existing.ProjectName = updatedProject.ProjectName;
            existing.ClientName = updatedProject.ClientName;
            existing.StartDate = updatedProject.StartDate;
            existing.EndDate = updatedProject.EndDate;
            existing.Budget = updatedProject.Budget;
            existing.Status = updatedProject.Status;
            existing.ManagerId = updatedProject.ManagerId;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
                return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

        // NEW CODE
        public async Task<ProjectSummaryDto> GetProjectSummaryAsync()
        {
            var projects = await _context.Projects.ToListAsync();

            return new ProjectSummaryDto
            {
                TotalProjects = projects.Count(),
                ActiveProjects = projects.Count(p => p.Status == ProjectStatus.Active),
                InactiveProjects = projects.Count(p => p.Status == ProjectStatus.Inactive),
                CompletedProjects = projects.Count(p => p.Status == ProjectStatus.Completed),
                TotalBudget = projects.Sum(p => p.Budget)
            };
        }
    }
}
