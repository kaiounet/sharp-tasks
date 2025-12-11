using System;
using System.ComponentModel.DataAnnotations;
using sharp_tasks.Validation;
using sharp_tasks.Enums;

namespace sharp_tasks.ViewModels.Task;

public class TaskEditVM
{
    [Required(ErrorMessage = "The label is required")]
    public string? Label { get; set; }
    [Required(ErrorMessage = "The description is required")]
    public string? Description { get; set; }
    [Required(ErrorMessage = "The limite date is required")]
    [DataType(DataType.Date)]
    [FutureDate(ErrorMessage = "The limite date must be today or a future date")]
    public DateTime LimiteDate { get; set; }
    [Required(ErrorMessage = "The state is required")]
    public State TaskState { get; set; }

}
