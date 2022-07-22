using System.Collections.Generic;
using System.Linq;
using Todos.Contract;
using Todos.Contract.Data;
using Todos.Contract.Messages;

namespace Todos.Backend.MessageHandlers
{
    public class ToggleAllCommandHandler : IToggleAllCommandHandling
    {
        private readonly ITodosRepository repo;

        public ToggleAllCommandHandler(ITodosRepository repo)
        {
            this.repo = repo;
        }

        public ICommandStatus Handle(ToggleAllCommand command)
        {
            var todos = repo.LoadTodos().ToList();
            todos = Toggle(todos, command.IsCompleted);
            repo.StoreTodos(todos.ToArray());
            return new Success();
        }

        private static List<Todo> Toggle(List<Todo> todos, bool isCompleted)
        {
            return todos.Select(t => new Todo(t.Id, t.Title, isCompleted)).ToList();
        }
    }
}
