namespace TaskFlow.CQRS.Application.Features.Tasks.Commands;

public record CreateTaskCommand(string Title, string Description);
