using Microsoft.EntityFrameworkCore;
using TaskFlow.CQRS.Application.Features.Tasks.Models;
using TaskFlow.CQRS.Data.Context;
using TaskFlow.CQRS.Infra.CQRS.Messaging;

namespace TaskFlow.CQRS.Application.Features.Tasks.Queries;


/// <summary>
/// Handles the <see cref="GetAllTasksQuery"/> by retrieving all task records from the database.
/// </summary>
public class GetAllTasksQueryHandler(ApplicationDbContext db) : IHandler<GetAllTasksQuery, IEnumerable<TaskModel>>
{
    public async Task<IEnumerable<TaskModel>> Handle(GetAllTasksQuery query, CancellationToken cancellationToken)
        => await db.Tasks.ToListAsync(cancellationToken);
}