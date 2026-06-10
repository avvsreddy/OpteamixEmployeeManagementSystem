using Microsoft.AspNetCore.Mvc;
using OpteamixEmployeeManagementSystem.Domain.Entities;
using OpteamixEmployeeManagementSystem.Domain.Repositories;

namespace OpteamixEmployeeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentsController(
            IDepartmentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult>
            GetDepartments()
        {
            var departments =
                await _repository
                    .GetDepartmentsAsync();

            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>
            GetDepartmentById(int id)
        {
            var department =
                await _repository
                    .GetDepartmentByIdAsync(id);

            if (department == null)
            {
                return NotFound(
                    $"Department with ID {id} not found");
            }

            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult>
            AddDepartment(
                Department department)
        {
            var result =
                await _repository
                    .AddDepartmentAsync(
                        department);

            return CreatedAtAction(
                nameof(GetDepartmentById),
                new
                {
                    id = result.DepartmentId
                },
                result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>
            UpdateDepartment(
                int id,
                Department department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest(
                    "Department ID mismatch");
            }

            var result =
                await _repository
                    .UpdateDepartmentAsync(
                        department);

            if (result == null)
            {
                return NotFound(
                    $"Department with ID {id} not found");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>
            DeleteDepartment(int id)
        {
            var result =
                await _repository
                    .DeleteDepartmentAsync(id);

            if (!result)
            {
                return NotFound(
                    $"Department with ID {id} not found");
            }

            return NoContent();
        }
    }
}
