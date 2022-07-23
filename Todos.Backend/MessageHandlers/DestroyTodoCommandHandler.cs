using System.Collections.Generic;
using System.Linq;
using Todos.Contract;
using Todos.Contract.Data;
using Todos.Contract.Messages;

namespace Todos.Backend.MessageHandlers
{
    public class DestroyTodoCommandHandler : IDestroyTodoCommandHandling
    {
        private readonly ITodosRepository _repo;

        public DestroyTodoCommandHandler(ITodosRepository repo)
        {
            _repo = repo;
        }

        public ICommandStatus Handle(DestroyTodoCommand command)
        {
            var todos = _repo.LoadTodos().ToList();
            todos = Destroy(todos, command.ID);
            _repo.StoreTodos(todos.ToArray());
            return new Success();
        }

        private static List<Todo> Destroy(List<Todo> todos, int id)
        {
            return todos.FindAll(t => t.Id != id).ToList();
        }
    }
}
