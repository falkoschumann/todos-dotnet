namespace Todos.Contract.Messages
{
    public readonly struct AddTodoCommand
    {
        public AddTodoCommand(string title)
        {
            Title = title;
        }

        public string Title { get; }
    }

    public interface IAddTodoCommandHandling
    {
        ICommandStatus Handle(AddTodoCommand command);
    }
}
