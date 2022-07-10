using Todos.Contract;
using Todos.Contract.Data;
using Todos.Contract.Messages;

namespace Todos.Backend.MessageHandlers
{
    public class DestroyTodoCommandHandler : IDestroyTodoCommandHandling
    {
        private readonly ITodosRepository repo;

        public DestroyTodoCommandHandler(ITodosRepository repo)
        {
            this.repo = repo;
        }

        public ICommandStatus Handle(DestroyTodoCommand command)
        {
            var todos = repo.LoadTodos().ToList();
            todos = Destroy(todos, command.ID);
            repo.StoreTodos(todos.ToArray());
            return new Success();
        }

        private static List<Todo> Destroy(List<Todo> todos, int id)
        {
            return todos.FindAll(t => t.Id != id).ToList();
        }
    }
}
