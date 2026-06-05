using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpteamixEmployeeManagementSystem.Domain.DTOs;
using OpteamixEmployeeManagementSystem.Domain.Entities;
using OpteamixEmployeeManagementSystem.Domain.Repositories;

namespace OpteamixEmployeeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    //[Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
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
        //[Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<ProjectDto>> Create([FromBody] CreateProjectDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var project = ProjectConversions.ToEntity(dto);
            var created = await _projectRepository.CreateProjectAsync(project);

            return CreatedAtAction(nameof(GetById),
                new { id = created.ProjectId },
                ProjectConversions.FromEntity(created));
        }

        // PUT api/projects/{id}
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<ProjectDto>> Update(int id, [FromBody] UpdateProjectDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var project = ProjectConversions.ToEntity(dto);
            var updated = await _projectRepository.UpdateProjectAsync(id, project);

            if (updated == null)
                return NotFound(new { message = $"Project with ID {id} not found." });

            return Ok(ProjectConversions.FromEntity(updated));
        }

        // DELETE api/projects/{id}
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _projectRepository.DeleteProjectAsync(id);

            if (!deleted)
                return NotFound(new { message = $"Project with ID {id} not found." });

            return NoContent();
        }
        // GET api/projects/summary
        [HttpGet("summary")]
        public async Task<IActionResult> GetProjectSummary()
        {
            var projects = await _projectRepository.GetAllProjectsAsync();

            var summary = new
            {
                TotalProjects = projects.Count(),
                ActiveProjects = projects.Count(p => p.Status == "Active"),
                InactiveProjects = projects.Count(p => p.Status == "Inactive"),
                CompletedProjects = projects.Count(p => p.Status == "Completed"),
                TotalBudget = projects.Sum(p => p.Budget)
            };

            return Ok(summary);
        }
    }
}