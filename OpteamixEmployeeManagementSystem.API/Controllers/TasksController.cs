using Microsoft.AspNetCore.Mvc;
using OpteamixEmployeeManagementSystem.Domain.DTOs;
using OpteamixEmployeeManagementSystem.Domain.Repositories;

namespace OpteamixEmployeeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _repository;

        public TasksController(
            ITaskRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks =
                await _repository.GetTasksAsync();

            return Ok(
                tasks.Select(
                    TaskConversions.FromEntity));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>
            GetTaskById(int id)
        {
            var task =
                await _repository.GetTaskByIdAsync(id);

            if (task == null)
                return NotFound();

            return Ok(
                TaskConversions.FromEntity(task));
        }

        [HttpPost]
        public async Task<IActionResult>
            AddTask(CreateTaskDto dto)
        {
            var task =
                TaskConversions.ToEntity(dto);

            var result =
                await _repository.AddTaskAsync(task);

            if (result == null)
            {
                return BadRequest(
                    "Invalid EmployeeId or ProjectId");
            }

            return Ok(
                TaskConversions.FromEntity(result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>
            UpdateTask(
                int id,
                UpdateTaskDto dto)
        {
            if (id != dto.TaskId)
            {
                return BadRequest(
                    "Task ID mismatch");
            }

            var task =
                TaskConversions.ToEntity(dto);

            var result =
                await _repository.UpdateTaskAsync(task);

            return Ok(
                TaskConversions.FromEntity(result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>
            DeleteTask(int id)
        {
            var result =
                await _repository.DeleteTaskAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok(
                "Task deleted successfully");
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult>
            GetTasksByEmployeeId(
                int employeeId)
        {
            var tasks =
                await _repository
                    .GetTasksByEmployeeIdAsync(
                        employeeId);

            return Ok(
                tasks.Select(
                    TaskConversions.FromEntity));
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult>
            GetTasksByStatus(
                string status)
        {
            var tasks =
                await _repository
                    .GetTasksByStatusAsync(
                        status);

            return Ok(
                tasks.Select(
                    TaskConversions.FromEntity));
        }

        [HttpGet("priority/{priority}")]
        public async Task<IActionResult>
            GetTasksByPriority(
                string priority)
        {
            var tasks =
                await _repository
                    .GetTasksByPriorityAsync(
                        priority);

            return Ok(
                tasks.Select(
                    TaskConversions.FromEntity));
        }

        [HttpPatch("{taskId}/assign/{employeeId}")]
        public async Task<IActionResult>
            AssignTask(
                int taskId,
                int employeeId)
        {
            var result =
                await _repository
                    .AssignTaskAsync(
                        taskId,
                        employeeId);

            if (!result)
            {
                return BadRequest(
                    "Employee not found or Task not found");
            }

            return Ok(
                "Task assigned successfully");
        }

        [HttpPatch("{taskId}/status/{status}")]
        public async Task<IActionResult>
            UpdateTaskStatus(
                int taskId,
                string status)
        {
            var result =
                await _repository
                    .UpdateTaskStatusAsync(
                        taskId,
                        status);

            if (!result)
            {
                return BadRequest(
                    "Invalid task or status");
            }

            return Ok(
                "Task status updated");
        }

        [HttpPatch("{taskId}/priority/{priority}")]
        public async Task<IActionResult>
            UpdateTaskPriority(
                int taskId,
                string priority)
        {
            var result =
                await _repository
                    .UpdateTaskPriorityAsync(
                        taskId,
                        priority);

            if (!result)
            {
                return BadRequest(
                    "Invalid task or priority");
            }

            return Ok(
                "Task priority updated");
        }
    }
}