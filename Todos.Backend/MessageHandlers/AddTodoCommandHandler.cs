using Todos.Contract;
using Todos.Contract.Data;
using Todos.Contract.Messages;

namespace Todos.Backend.MessageHandlers
{
    public class AddTodoCommandHandler : IAddTodoCommandHandling
    {
        private readonly ITodosRepository repo;

        public AddTodoCommandHandler(ITodosRepository repo)
        {
            this.repo = repo;
        }

        public ICommandStatus Handle(AddTodoCommand command)
        {
            var todos = repo.LoadTodos().ToList();
            todos = Add(todos, command.Title);
            repo.StoreTodos(todos.ToArray());
            return new Success();
        }

        private static List<Todo> Add(List<Todo> todos, string title)
        {
            if (title.Length == 0)
            {
                return todos;
            }

            var id = todos.Select(t => t.ID).Max();
            id++;
            var todo = new Todo(id, title, IsCompleted: false);
            todos.Add(todo);
            return todos;
        }
    }
}
