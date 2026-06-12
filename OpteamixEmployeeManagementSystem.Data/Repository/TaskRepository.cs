using Microsoft.EntityFrameworkCore;
using OpteamixEmployeeManagementSystem.Domain.Entities;
using OpteamixEmployeeManagementSystem.Domain.Enums;
using OpteamixEmployeeManagementSystem.Domain.Repositories;

namespace OpteamixEmployeeManagementSystem.Data.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly EmployeeDbContext _context;

        public TaskRepository(
            EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItem>> GetTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskItem?> GetTaskByIdAsync(
            int taskId)
        {
            return await _context.Tasks
                .FirstOrDefaultAsync(
                    t => t.TaskItemId == taskId);
        }

        public async Task<TaskItem?> AddTaskAsync(
            TaskItem task)
        {
            if (task.EmployeeId.HasValue)
            {
                var employeeExists =
                    await _context.Employees.AnyAsync(
                        e => e.EmployeeId == task.EmployeeId);

                if (!employeeExists)
                {
                    return null;
                }
            }

            var projectExists =
                await _context.Projects.AnyAsync(
                    p => p.ProjectId == task.ProjectId);

            if (!projectExists)
            {
                return null;
            }

            await _context.Tasks.AddAsync(task);

            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<TaskItem> UpdateTaskAsync(
            TaskItem task)
        {
            var existingTask =
                await _context.Tasks
                .FirstOrDefaultAsync(
                    t => t.TaskItemId == task.TaskItemId);

            if (existingTask != null)
            {
                existingTask.Title =
                    task.Title;

                existingTask.Description =
                    task.Description;

                existingTask.Status =
                    task.Status;

                existingTask.Priority =
                    task.Priority;

                existingTask.EmployeeId =
                    task.EmployeeId;

                existingTask.ProjectId =
                    task.ProjectId;

                await _context.SaveChangesAsync();
            }

            return task;
        }

        public async Task<bool> DeleteTaskAsync(
            int taskId)
        {
            var task =
                await _context.Tasks
                .FirstOrDefaultAsync(
                    t => t.TaskItemId == taskId);

            if (task == null)
                return false;

            _context.Tasks.Remove(task);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<TaskItem>>
            GetTasksByEmployeeIdAsync(
                int employeeId)
        {
            return await _context.Tasks
                .Where(
                    t => t.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task<List<TaskItem>>
            GetTasksByStatusAsync(
                string status)
        {
            return await _context.Tasks
                .Where(
                    t => t.Status.ToString() == status)
                .ToListAsync();
        }

        public async Task<List<TaskItem>>
            GetTasksByPriorityAsync(
                string priority)
        {
            return await _context.Tasks
                .Where(
                    t => t.Priority.ToString() == priority)
                .ToListAsync();
        }

        public async Task<bool> AssignTaskAsync(
            int taskId,
            int employeeId)
        {
            var task =
                await _context.Tasks
                .FirstOrDefaultAsync(
                    t => t.TaskItemId == taskId);

            if (task == null)
                return false;

            var employeeExists =
                await _context.Employees.AnyAsync(
                    e => e.EmployeeId == employeeId);

            if (!employeeExists)
                return false;

            task.EmployeeId = employeeId;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool>
            UpdateTaskStatusAsync(
                int taskId,
                string status)
        {
            var task =
                await _context.Tasks
                .FirstOrDefaultAsync(
                    t => t.TaskItemId == taskId);

            if (task == null)
                return false;

            if (!Enum.TryParse<TaskItemStatus>(
                status,
                true,
                out var taskStatus))
            {
                return false;
            }

            task.Status = taskStatus;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool>
            UpdateTaskPriorityAsync(
                int taskId,
                string priority)
        {
            var task =
                await _context.Tasks
                .FirstOrDefaultAsync(
                    t => t.TaskItemId == taskId);

            if (task == null)
                return false;

            if (!Enum.TryParse<TaskPriority>(
                priority,
                true,
                out var taskPriority))
            {
                return false;
            }

            task.Priority = taskPriority;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}