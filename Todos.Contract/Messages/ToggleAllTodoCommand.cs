namespace Todos.Contract.Messages
{
    public readonly record struct ToggleAllCommand(bool IsCompleted);

    public interface IToggleAllCommandHandling
    {
        ICommandStatus Handle(ToggleAllCommand command);
    }
}
