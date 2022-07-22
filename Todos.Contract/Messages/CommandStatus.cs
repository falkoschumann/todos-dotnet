namespace Todos.Contract.Messages
{
    public interface ICommandStatus { }

    public readonly struct Success : ICommandStatus { };

    public readonly struct Failure : ICommandStatus
    {
        public Failure(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }
    }
}
