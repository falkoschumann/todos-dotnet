namespace Todos.Contract.Messages
{
    public interface ICommandStatus { }

    public readonly record struct Success() : ICommandStatus;

    public readonly record struct Failure(string ErrorMessage) : ICommandStatus;
}
