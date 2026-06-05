using Microsoft.AspNetCore.Mvc;
using OpteamixEmployeeManagementSystem.Domain.Entities;
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

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>
            GetTaskById(int id)
        {
            var task =
                await _repository.GetTaskByIdAsync(id);

            if (task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult>
            AddTask(TaskItem task)
        {
            var result =
                await _repository.AddTaskAsync(task);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>
            UpdateTask(
                int id,
                TaskItem task)
        {
            if (id != task.TaskId)
            {
                return BadRequest(
                    "Task ID mismatch");
            }

            var result =
                await _repository.UpdateTaskAsync(task);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>
            DeleteTask(int id)
        {
            var result =
                await _repository.DeleteTaskAsync(id);

            return Ok(result);
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

            return Ok(tasks);
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

            return Ok(tasks);
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

            return Ok(tasks);
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
                return NotFound();

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
                return BadRequest(
                    "Invalid task or status");

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
                return BadRequest(
                    "Invalid task or priority");

            return Ok(
                "Task priority updated");
        }
    }
}