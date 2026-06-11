using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using OpteamixEmployeeManagementSystem.Domain.BusinessValidators;
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
        [ProducesResponseType(typeof(IQueryable<Project>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAll()
        {
            var projects = await _projectRepository.GetAllProjectsAsync();
            return Ok(projects.Select(p => ProjectConversions.FromEntity(p)));
        }

        // GET api/projects ODATA
        [HttpGet("odata")]
        [EnableQuery]
        [ProducesResponseType(typeof(IQueryable<Project>), StatusCodes.Status200OK)]
        public IActionResult GetAllOdata()
        {
            var projects = _projectRepository.GetAllProjectsQueryable();
            return Ok(projects);
        }

        // GET api/projects/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<Project>(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProjectDto>> GetById(int id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);

            if (project == null)
                return NotFound(new { message = $"Project with ID {id} not found." });

            return Ok(ProjectConversions.FromEntity(project));
        }

        // GET api/projects/summary
        [HttpGet("summary")]
        [ProducesResponseType<Project>(StatusCodes.Status200OK)]
        public async Task<ActionResult<ViewProjectSummaryDto>> GetViewProjectSummary()
        {
            var summary = await _projectRepository.GetViewProjectSummaryAsync();

            return Ok(summary);
        }

        // POST api/projects
        [HttpPost]
        [ProducesResponseType<Project>(StatusCodes.Status201Created)]
        public async Task<ActionResult<ProjectDto>> Create([FromBody] CreateProjectDto dto)
        {
            _projectValidator.Validate(dto);

            var project = ProjectConversions.ToEntity(dto);
            var created = await _projectRepository.CreateProjectAsync(project);

            return CreatedAtAction(nameof(GetById),
                new { id = created.ProjectId },
                ProjectConversions.FromEntity(created));
        }

        // PUT api/projects/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<Project>(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProjectDto>> Update(int id, [FromBody] UpdateProjectDto dto)
        {
            _projectValidator.Validate(dto);

            var project = ProjectConversions.ToEntity(dto);
            var updated = await _projectRepository.UpdateProjectAsync(id, project);

            if (updated == null)
                return NotFound(new { message = $"Project with ID {id} not found." });

            return Ok(ProjectConversions.FromEntity(updated));
        }

        // DELETE api/projects/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<Project>(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _projectRepository.DeleteProjectAsync(id);

            if (!deleted)
                return NotFound(new { message = $"Project with ID {id} not found." });

            return NoContent();
        }
    }
}