using TaskFlow.CQRS.Application.Features.Tasks.Models;
using TaskFlow.CQRS.Data.Context;
using TaskFlow.CQRS.Infra.CQRS.Messaging;

namespace TaskFlow.CQRS.Application.Features.Tasks.Commands;

/// <summary>
/// Handles the <see cref="CreateTaskCommand"/> by creating a new task entity
/// and persisting it to the database.
/// </summary>
public class CreateTaskCommandHandler(ApplicationDbContext db) : IHandler<CreateTaskCommand, Guid>
{
    public async Task<Guid> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
    {
        //Validate the command... (Fluent Validation, etc.)
        var task = new TaskModel(command.Title, command.Description);

        db.Tasks.Add(task);
        await db.SaveChangesAsync(cancellationToken);

        return task.Id;
    }
}