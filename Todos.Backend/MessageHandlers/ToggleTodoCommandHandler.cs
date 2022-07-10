using Todos.Contract;
using Todos.Contract.Data;
using Todos.Contract.Messages;

namespace Todos.Backend.MessageHandlers
{
    public class ToggleTodoCommandHandler : IToggleTodoCommandHandling
    {
        private readonly ITodosRepository repo;

        public ToggleTodoCommandHandler(ITodosRepository repo)
        {
            this.repo = repo;
        }

        public ICommandStatus Handle(ToggleTodoCommand command)
        {
            var todos = repo.LoadTodos().ToList();
            todos = Toggle(todos, command.ID);
            repo.StoreTodos(todos.ToArray());
            return new Success();
        }

        private static List<Todo> Toggle(List<Todo> todos, int id)
        {
            return todos.Select(t => t.Id == id ? new Todo(t.Id, t.Title, !t.IsCompleted) : t).ToList();
        }
    }
}
