# TaskFlow 🧠
## CQRS with Custom Mediator — No MediatR

This is a simple example of the **CQRS** pattern (Command Query Responsibility Segregation), using **.NET 9**, with no external libraries like MediatR.

It is a small task manager API (like a to-do list).  
We use **commands** to create tasks and **queries** to read them.

## ⚠️ Note: This project does **not use MediatR**.  
MediatR will be a paid library, so we built a **simple, clean, and free** custom solution.

## ✨ Technologies

- .NET 9
- C# 13
- Minimal API
- CQRS pattern
- Scrutor (for handler discovery)
- SQLite In-Memory (EF Core)
- OpenAPI (native from .NET 9)

## 🧱 Project Structure

| Project                  | Description                           |
|--------------------------|---------------------------------------|
| `TaskFlow.CQRS.Api`      | Entry point, API, DI config, Swagger  |
| `TaskFlow.CQRS.Application` | Commands, Queries, Handlers        |
| `TaskFlow.CQRS.Data`     | EF Core DbContext and Entities        |
| `TaskFlow.CQRS.Infra`    | Custom Mediator, Interfaces, Extensions |

## ⚖️ Comparison with MediatR

| MediatR                     | This Project                  | Status / Suggestion            |
|----------------------------|-------------------------------|--------------------------------|
| `IMediator.Send(...)`      | ✅ `IMediator.Send(...)`       | Implemented                    |
| `IMediator.Publish(...)`   | *(not implemented)*           | Can be added in the future     |
| `INotificationHandler<T>`  | *(not implemented)*           | Could be the next improvement  |

## 🧠 Custom Mediator

This project includes a simple custom `IMediator` implementation to replace MediatR.

It is located in:

```
TaskFlow.CQRS.Infra/CQRS/Messaging/
```

### Key files:
- `IMediator.cs`: Interface with `Send<TRequest, TResult>()`
- `Mediator.cs`: Resolves handlers using `IServiceProvider`
- `IHandler.cs`: Base interface for all command and query handlers

### 🔄 Handler Registration with Scrutor

All handlers are registered automatically using **Scrutor**, in:

```
TaskFlow.CQRS.Infra/Extensions/ServiceCollectionExtensions.cs
```

You can find the registration method `AddMediator(...)`, which scans the `Application` layer for all `IHandler<,>` implementations and adds them to the service container.

In the API project (`Program.cs`), we call it like this:

```csharp
builder.Services.AddMediator(typeof(CreateTaskCommandHandler).Assembly);
```

This setup allows clean, simple message dispatching without external libraries.

## 🚀 How to Run

Clone the project:

```bash
git clone https://github.com/vagnerjsmello/task-flow-cqrs-without-mediatr.git
cd task-flow-cqrs-without-mediatr
```

Restore and run the project:

```bash
dotnet restore
dotnet run --project TaskFlow.CQRS.Api
```



## 📄 API Documentation

This project uses native OpenAPI (no Swagger UI inside the app).

To view the API using Swagger Editor:

1. Go to [https://editor.swagger.io](https://editor.swagger.io)
2. Paste one of these URLs:

```
https://localhost:7236/openapi/v1.json
```
or
```
http://localhost:7237/openapi/v1.json
```



## 🛠️ Example Endpoints

### ✅ Create a new task  
`POST /tasks`

```json
{
  "title": "Study CQRS",
  "description": "Create a small example using Minimal API"
}
```

### 📄 Get all tasks  
`GET /tasks`


## 🎯 Goal

This project is for learning and teaching.  
It shows how to use the CQRS pattern in a **simple**, **modern**, and **clean** way using only .NET.

Use it to study or as a base for bigger systems.  
You can also extend it with:
- Validation (FluentValidation)
- Domain Events
- Publish/Subscribe
- Event Sourcing

## 📄 License

This project uses the [MIT License](LICENSE).
