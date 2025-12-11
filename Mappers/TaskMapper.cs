using sharp_tasks.ViewModels.Task;

namespace sharp_tasks.Mappers;

public class TaskMapper
{
    public static Models.Task GetTaskFromTaskAddVM(TaskAddVM model)
    {
        return new Models.Task
        {
            Label = model.Label,
            Description = model.Description,
            LimiteDate = model.LimiteDate,
            TaskState = model.TaskState
        };

    }

    public static List<TaskDisplayVM> GetTaskDisplayVMsFromTasks(List<Models.Task> tasks)
    {
        List<TaskDisplayVM> taskDisplayVMs = new List<TaskDisplayVM>();
        if (tasks == null) { return taskDisplayVMs; }
        foreach (Models.Task task in tasks)
        {
            TaskDisplayVM taskDisplayVM = new TaskDisplayVM
            {
                Label = task.Label,
                Description = task.Description,
                LimiteDate = task.LimiteDate,
                TaskState = task.TaskState
            };
            taskDisplayVMs.Add(taskDisplayVM);
        }
        return taskDisplayVMs;
    }

    public static Models.Task GetTaskFromTaskEditVM(TaskEditVM model)
    {
        return new Models.Task
        {
            Label = model.Label,
            Description = model.Description,
            LimiteDate = model.LimiteDate,
            TaskState = model.TaskState
        };

    }

    public static TaskEditVM GetTaskEditVMFromTask(Models.Task task)
    {
        return new TaskEditVM
        {
            Label = task.Label,
            Description = task.Description,
            LimiteDate = task.LimiteDate,
            TaskState = task.TaskState
        };
    }

}
