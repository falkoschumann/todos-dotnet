namespace Todos.Contract.Messages
{
    public readonly record struct SaveTodoCommand(int Id, string Title);

    public interface ISaveTodoCommandHandling
    {
        ICommandStatus Handle(SaveTodoCommand command);
    }
}
