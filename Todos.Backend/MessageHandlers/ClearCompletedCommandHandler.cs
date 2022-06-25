using Todos.Contract;
using Todos.Contract.Data;
using Todos.Contract.Messages;

namespace Todos.Backend.MessageHandlers
{
    public class ClearCompletedCommandHandler : IClearCompletedCommandHandling
    {
        private readonly ITodosRepository repo;

        public ClearCompletedCommandHandler(ITodosRepository repo)
        {
            this.repo = repo;
        }

        public ICommandStatus Handle(ClearCompletedCommand command)
        {
            var todos = repo.LoadTodos().ToList();
            todos = Clear(todos);
            repo.StoreTodos(todos.ToArray());
            return new Success();
        }

        private static List<Todo> Clear(List<Todo> todos)
        {
            return todos.FindAll(t => !t.IsCompleted).ToList();
        }
    }
}
