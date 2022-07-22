namespace Todos.Contract.Messages
{
    public readonly struct DestroyTodoCommand
    {
        public DestroyTodoCommand(int id)
        {
            ID = id;
        }

        public int ID { get; }
    }

    public interface IDestroyTodoCommandHandling
    {
        ICommandStatus Handle(DestroyTodoCommand command);
    }
}
