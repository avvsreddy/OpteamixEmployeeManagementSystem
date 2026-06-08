using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpteamixEmployeeManagementSystem.Domain.BusinessValidator;
using OpteamixEmployeeManagementSystem.Domain.DTOs;
using OpteamixEmployeeManagementSystem.Domain.Entities;
using OpteamixEmployeeManagementSystem.Domain.Repositories;

namespace OpteamixEmployeeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectValidator _projectValidator;

        public ProjectController(IProjectRepository projectRepository, IProjectValidator projectValidator)
        {
            _projectRepository = projectRepository;
            _projectValidator = projectValidator;
        }

        // GET api/projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAll()
        {
            var projects = await _projectRepository.GetAllProjectsAsync();
            return Ok(projects.Select(p => ProjectConversions.FromEntity(p)));
        }

        // GET api/projects/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetById(int id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);

            if (project == null)
                return NotFound(new { message = $"Project with ID {id} not found." });

            return Ok(ProjectConversions.FromEntity(project));
        }

        // POST api/projects
        [HttpPost]
        public async Task<ActionResult<ProjectDto>> Create([FromBody] CreateProjectDto dto)
        {
            var project = ProjectConversions.ToEntity(dto);
            _projectValidator.Validate(dto);

            var created = await _projectRepository.CreateProjectAsync(project);

            return CreatedAtAction(nameof(GetById),
                new { id = created.ProjectId },
                ProjectConversions.FromEntity(created));
        }

        // PUT api/projects/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectDto>> Update(int id, [FromBody] UpdateProjectDto dto)
        {
            var project = ProjectConversions.ToEntity(dto);
            _projectValidator.Validate(dto);

            var updated = await _projectRepository.UpdateProjectAsync(id, project);

            if (updated == null)
                return NotFound(new { message = $"Project with ID {id} not found." });

            return Ok(ProjectConversions.FromEntity(updated));
        }

        // DELETE api/projects/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _projectRepository.DeleteProjectAsync(id);

            if (!deleted)
                return NotFound(new { message = $"Project with ID {id} not found." });

            return NoContent();
        }

        // GET api/projects/summary
        [HttpGet("summary")]
        public async Task<ActionResult<ProjectSummaryDto>> GetProjectSummary()
        {
            var summary = await _projectRepository.GetProjectSummaryAsync();

            return Ok(summary);
        }
    }
}