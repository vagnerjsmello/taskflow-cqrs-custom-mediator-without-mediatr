namespace TaskFlow.CQRS.Application.Features.Tasks.Models;

public class TaskModel : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public TaskModel(string title, string description)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Description = description ?? throw new ArgumentNullException(nameof(description));
    }
}


