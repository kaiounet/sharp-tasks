using Microsoft.AspNetCore.Mvc;
using sharp_tasks.Enums;
using sharp_tasks.Filters;
using sharp_tasks.Helpers;
using sharp_tasks.Mappers;
using sharp_tasks.Services;
using sharp_tasks.ViewModels.Task;
using System.Text.Json;

namespace sharp_tasks.Controllers;

[TypeFilter(typeof(AuthenticationFilter))]
[TypeFilter(typeof(ThemeFilter))]
public class TasksController : Controller
{
    private readonly ISessionManagerService _sessionManager;

    public TasksController(ISessionManagerService sessionManager)
    {
        _sessionManager = sessionManager ?? throw new ArgumentNullException(nameof(sessionManager));
    }

    public IActionResult Index()
    {
        List<Models.Task> tasks = _sessionManager.Get<List<Models.Task>>("tasks");
        List<TaskDisplayVM> taskDisplayVMs = TaskMapper.GetTaskDisplayVMsFromTasks(tasks);
        ViewBag.tasks = taskDisplayVMs;
        return View();
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(TaskAddVM model)
    {
        Models.Task task;
        List<Models.Task> tasks;

        if (!ModelState.IsValid)
        {
            return View();
        }

        task = TaskMapper.GetTaskFromTaskAddVM(model);
        tasks = _sessionManager.Get<List<Models.Task>>("tasks") ?? new List<Models.Task>();

        tasks.Add(task);
        _sessionManager.Add("tasks", tasks);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int index)
    {
        List<Models.Task> tasks = _sessionManager.Get<List<Models.Task>>("tasks");

        if (index < 0 || index >= tasks.Count)
        {
            return NotFound();
        }

        TaskEditVM taskEditVM = TaskMapper.GetTaskEditVMFromTask(tasks[index]);
        ViewBag.taskIndex = index;
        return View(taskEditVM);
    }

    [HttpPost]
    public IActionResult Edit(int index, TaskEditVM model)
    {
        List<Models.Task> tasks;

        if (!ModelState.IsValid)
        {
            ViewBag.taskIndex = index;
            return View(model);
        }

        tasks = _sessionManager.Get<List<Models.Task>>("tasks");

        if (index < 0 || index >= tasks.Count)
        {
            return NotFound();
        }

        tasks[index] = TaskMapper.GetTaskFromTaskEditVM(model);
        _sessionManager.Add("tasks", tasks);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Delete(int index)
    {
        List<Models.Task> tasks = _sessionManager.Get<List<Models.Task>>("tasks");

        if (index >= 0 && index < tasks.Count)
        {
            tasks.RemoveAt(index);
            _sessionManager.Add("tasks", tasks);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult IncrementState(int index)
    {
        List<Models.Task> tasks = _sessionManager.Get<List<Models.Task>>("tasks");

        if (index >= 0 && index < tasks.Count)
        {
            var currentState = tasks[index].TaskState;
            var newState = StateHelper.GetNextState(currentState);

            if (StateHelper.IsValidTransition(currentState, newState))
            {
                tasks[index].TaskState = newState;
                _sessionManager.Add("tasks", tasks);
            }
        }

        return RedirectToAction(nameof(Index));
    }
}