namespace Todos.Contract.Messages
{
    public readonly record struct AddTodoCommand(string Title);

    public interface IAddTodoCommandHandling
    {
        ICommandStatus Handle(AddTodoCommand command);
    }
}
