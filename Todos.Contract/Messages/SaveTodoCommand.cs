namespace Todos.Contract.Messages
{
    public readonly struct SaveTodoCommand
    {
        public SaveTodoCommand(int id, string title)
        {
            ID = id;
            Title = title;
        }

        public int ID { get; }
        public string Title { get; }
    }

    public interface ISaveTodoCommandHandling
    {
        ICommandStatus Handle(SaveTodoCommand command);
    }
}
