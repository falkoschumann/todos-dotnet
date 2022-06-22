namespace Todos.Contract.Messages
{
    public readonly record struct ToggleTodoCommand(int ID);

    public interface IToggleTodoCommandHandling
    {
        ICommandStatus Handle(ToggleTodoCommand command);
    }
}
