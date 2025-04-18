using TaskFlow.CQRS.Application.Features.Tasks.Commands;
using TaskFlow.CQRS.Application.Features.Tasks.Models;
using TaskFlow.CQRS.Application.Features.Tasks.Queries;
using TaskFlow.CQRS.Data.Context;
using TaskFlow.CQRS.Data.Extensions;
using TaskFlow.CQRS.Infra.CQRS.Messaging;
using TaskFlow.CQRS.Infra.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();


// Registers the IBus implementation to enable message dispatching.
// Uses Scrutor to scan the specified assembly for all classes implementing IHandler<,>
// and registers them automatically as their implemented interfaces with scoped lifetime.
builder.Services.AddCqrs(typeof(CreateTaskCommandHandler).Assembly);

// Registers the ApplicationDbContext using SQLite In-Memory.
builder.Services.AddPersistence();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Ensure the in-memory SQLite database schema is created
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowAll");
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Endpoints
app.MapPost("/tasks", async (CreateTaskCommand command, IMediator mediator, CancellationToken ct) =>
{
    var id = await mediator.Send<CreateTaskCommand, Guid>(command, ct);
    return Results.Created($"/tasks/{id}", new { Id = id });
});

app.MapGet("/tasks", async (IMediator mediator, CancellationToken ct) =>
{
    var result = await mediator.Send<GetAllTasksQuery, IEnumerable<TaskModel>>(new(), ct);
    return Results.Ok(result);
});

app.Run();
