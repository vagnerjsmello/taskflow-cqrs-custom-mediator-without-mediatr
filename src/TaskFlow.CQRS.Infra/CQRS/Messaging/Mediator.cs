using Microsoft.Extensions.DependencyInjection;

namespace TaskFlow.CQRS.Infra.CQRS.Messaging;

/// <summary>
/// Default implementation of <see cref="IMediator"/> that resolves and invokes 
/// message handlers using dependency injection based on the message type.
/// </summary>
/// <param name="serviceProvider">The <see cref="IServiceProvider"/> used to resolve handlers.</param>
public class Mediator(IServiceProvider serviceProvider) : IMediator
{
    public Task<TResult> Send<TMessage, TResult>(TMessage message, CancellationToken cancellationToken = default)
    {
        var handler = serviceProvider.GetRequiredService<IHandler<TMessage, TResult>>();
        return handler.Handle(message, cancellationToken);
    }   
}

