using System.Collections.Generic;
using System.Linq;
using Todos.Contract;
using Todos.Contract.Data;
using Todos.Contract.Messages;

namespace Todos.Backend.MessageHandlers
{
    public class ToggleTodoCommandHandler : IToggleTodoCommandHandling
    {
        private readonly ITodosRepository _repo;

        public ToggleTodoCommandHandler(ITodosRepository repo)
        {
            _repo = repo;
        }

        public ICommandStatus Handle(ToggleTodoCommand command)
        {
            var todos = _repo.LoadTodos().ToList();
            todos = Toggle(todos, command.ID);
            _repo.StoreTodos(todos.ToArray());
            return new Success();
        }

        private static List<Todo> Toggle(List<Todo> todos, int id)
        {
            return todos.Select(t => t.Id == id ? new Todo(t.Id, t.Title, !t.IsCompleted) : t).ToList();
        }
    }
}
