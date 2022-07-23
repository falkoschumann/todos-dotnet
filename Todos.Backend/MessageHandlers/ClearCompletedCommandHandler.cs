using System.Collections.Generic;
using System.Linq;
using Todos.Contract;
using Todos.Contract.Data;
using Todos.Contract.Messages;

namespace Todos.Backend.MessageHandlers
{
    public class ClearCompletedCommandHandler : IClearCompletedCommandHandling
    {
        private readonly ITodosRepository _repo;

        public ClearCompletedCommandHandler(ITodosRepository repo)
        {
            _repo = repo;
        }

        public ICommandStatus Handle(ClearCompletedCommand command)
        {
            var todos = _repo.LoadTodos().ToList();
            todos = Clear(todos);
            _repo.StoreTodos(todos.ToArray());
            return new Success();
        }

        private static List<Todo> Clear(List<Todo> todos)
        {
            return todos.FindAll(t => !t.IsCompleted).ToList();
        }
    }
}
