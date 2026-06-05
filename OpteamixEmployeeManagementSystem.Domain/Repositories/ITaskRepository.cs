using OpteamixEmployeeManagementSystem.Domain.Entities;

namespace OpteamixEmployeeManagementSystem.Domain.Repositories
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetTasksAsync();

        Task<TaskItem?> GetTaskByIdAsync(int taskId);

        Task<TaskItem> AddTaskAsync(TaskItem task);

        Task<TaskItem> UpdateTaskAsync(TaskItem task);

        Task<bool> DeleteTaskAsync(int taskId);

        Task<List<TaskItem>>
            GetTasksByEmployeeIdAsync(int employeeId);

        Task<List<TaskItem>>
            GetTasksByStatusAsync(string status);

        Task<List<TaskItem>>
            GetTasksByPriorityAsync(string priority);

        Task<bool>
            AssignTaskAsync(
                int taskId,
                int employeeId);

        Task<bool>
            UpdateTaskStatusAsync(
                int taskId,
                string status);

        Task<bool>
            UpdateTaskPriorityAsync(
                int taskId,
                string priority);
    }
}