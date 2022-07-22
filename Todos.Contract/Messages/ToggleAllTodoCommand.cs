namespace Todos.Contract.Messages
{
    public readonly struct ToggleAllCommand
    {
        public ToggleAllCommand(bool isCompleted)
        {
            IsCompleted = isCompleted;
        }

        public bool IsCompleted { get; }
    }

    public interface IToggleAllCommandHandling
    {
        ICommandStatus Handle(ToggleAllCommand command);
    }
}
