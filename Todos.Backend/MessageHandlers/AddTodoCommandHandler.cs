using System.Collections.Generic;
using System.Linq;
using Todos.Contract;
using Todos.Contract.Data;
using Todos.Contract.Messages;

namespace Todos.Backend.MessageHandlers
{
    public class AddTodoCommandHandler : IAddTodoCommandHandling
    {
        private readonly ITodosRepository _repo;

        public AddTodoCommandHandler(ITodosRepository repo)
        {
            _repo = repo;
        }

        public ICommandStatus Handle(AddTodoCommand command)
        {
            var todos = _repo.LoadTodos().ToList();
            todos = Add(todos, command.Title);
            _repo.StoreTodos(todos.ToArray());
            return new Success();
        }

        private static List<Todo> Add(List<Todo> todos, string title)
        {
            if (title.Length == 0)
            {
                return todos;
            }

            var id = todos.Select(t => t.Id).DefaultIfEmpty(0).Max();
            id++;
            var todo = new Todo(id, title);
            todos.Add(todo);
            return todos;
        }
    }
}
