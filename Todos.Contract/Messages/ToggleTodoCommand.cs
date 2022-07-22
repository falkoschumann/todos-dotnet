namespace Todos.Contract.Messages
{
    public readonly struct ToggleTodoCommand
    {
        public ToggleTodoCommand(int id) {
            ID = id;
        }

        public int ID { get; }
    }

    public interface IToggleTodoCommandHandling
    {
        ICommandStatus Handle(ToggleTodoCommand command);
    }
}
