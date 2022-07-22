namespace Todos.Contract.Messages
{
    public readonly struct ClearCompletedCommand { }

    public interface IClearCompletedCommandHandling
    {
        ICommandStatus Handle(ClearCompletedCommand command);
    }
}
