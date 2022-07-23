using System.Collections.Generic;
using System.Linq;
using Todos.Contract;
using Todos.Contract.Data;
using Todos.Contract.Messages;

namespace Todos.Backend.MessageHandlers
{
    public class ToggleAllCommandHandler : IToggleAllCommandHandling
    {
        private readonly ITodosRepository _repo;

        public ToggleAllCommandHandler(ITodosRepository repo)
        {
            _repo = repo;
        }

        public ICommandStatus Handle(ToggleAllCommand command)
        {
            var todos = _repo.LoadTodos().ToList();
            todos = Toggle(todos, command.IsCompleted);
            _repo.StoreTodos(todos.ToArray());
            return new Success();
        }

        private static List<Todo> Toggle(List<Todo> todos, bool isCompleted)
        {
            return todos.Select(t => new Todo(t.Id, t.Title, isCompleted)).ToList();
        }
    }
}
