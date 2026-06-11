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
                .Where(p => !p.isDeleted)
                .Include(p => p.Manager)
                .ToListAsync();
        }

        public async Task<Project?> GetProjectByIdAsync(int id)
        {
            return await _context.Projects
                .Include(p => p.Manager)
                .FirstOrDefaultAsync(p => p.ProjectId == id && !p.isDeleted);
        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Project?> UpdateProjectAsync(int id, Project updatedProject)
        {
            var existing = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id && !p.isDeleted);

            if (existing == null)
            {
                return null;
            }

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
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id && !p.isDeleted);

            if (project == null)
                return false;

            project.isDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ViewProjectSummaryDto> GetViewProjectSummaryAsync()
        {
            var projects = await _context.Projects.Where(p => !p.isDeleted).ToListAsync();

            return new ViewProjectSummaryDto
            {
                TotalProjects = projects.Count(),

                ActiveProjects = projects.Count(p => p.Status == "Active"),

                InactiveProjects = projects.Count(p => p.Status == "InActive"),

                CompletedProjects = projects.Count(p => p.Status == "Completed"),

                TotalBudget = projects.Sum(p => p.Budget)
            };
        }

        public IQueryable<Project> GetAllProjectsQueryable()
        {
            return _context.Projects
            .Where(p => !p.isDeleted)
            .AsQueryable();
        }
    }
}
