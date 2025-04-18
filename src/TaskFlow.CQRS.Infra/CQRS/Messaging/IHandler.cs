namespace TaskFlow.CQRS.Infra.CQRS.Messaging;

/// <summary>
/// Defines a generic handler for processing a message of type <typeparamref name="TMessage"/>
/// and returning a result of type <typeparamref name="TResult"/>.
/// </summary>
/// <typeparam name="TMessage">The type of the message to handle.</typeparam>
/// <typeparam name="TResult">The type of the result returned after handling the message.</typeparam>
public interface IHandler<in TMessage, TResult>
{
    Task<TResult> Handle(TMessage message, CancellationToken cancellationToken = default);
}
