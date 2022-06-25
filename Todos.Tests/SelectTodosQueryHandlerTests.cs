using Todos.Backend.Adapters;
using Todos.Backend.MessageHandlers;
using Todos.Contract.Data;
using Todos.Contract.Messages;

namespace Todos.Tests
{
    public class SelectTodosQueryHandlerTests
    {
        [Test]
        public void SelectTodos_ReturnsAllTodos()
        {
            var repo = new MemoryTodosRepository();
            Todo[] todos = {
                new Todo(ID : 1, Title: "Taste JavaScript", IsCompleted: true),
                new Todo(ID : 2, Title: "Buy Unicorn", IsCompleted: false),
            };
            repo.StoreTodos(todos);
            var handler = new SelectTodosQueryHandler(repo);

            var result = handler.Handle(new SelectTodosQuery());

            Todo[] expected = {
                new Todo(ID : 1, Title: "Taste JavaScript", IsCompleted: true),
                new Todo(ID : 2, Title: "Buy Unicorn", IsCompleted: false),
            };
            Assert.That(result.Todos, Is.EqualTo(expected));
        }
    }
}
