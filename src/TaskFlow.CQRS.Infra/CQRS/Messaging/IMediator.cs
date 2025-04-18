namespace TaskFlow.CQRS.Infra.CQRS.Messaging;

/// <summary>
/// Defines a simple message bus that dispatches messages to their 
/// corresponding handlers, based solely on the message type.
/// </summary>
public interface IMediator
{
    Task<TResult> Send<TMessage, TResult>(TMessage message, CancellationToken cancellationToken = default);    
}

