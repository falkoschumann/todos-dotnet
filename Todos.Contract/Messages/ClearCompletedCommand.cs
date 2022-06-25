namespace Todos.Contract.Messages
{
    public readonly record struct ClearCompletedCommand();

    public interface IClearCompletedCommandHandling
    {
        ICommandStatus Handle(ClearCompletedCommand command);
    }
}
