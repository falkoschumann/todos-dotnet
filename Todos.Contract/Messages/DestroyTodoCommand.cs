namespace Todos.Contract.Messages
{
    public readonly record struct DestroyTodoCommand(int ID);

    public interface IDestroyTodoCommandHandling
    {
        ICommandStatus Handle(DestroyTodoCommand command);
    }
}
