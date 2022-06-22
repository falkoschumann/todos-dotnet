using Todos.Backend.Adapters;
using Todos.Backend.MessageHandler;
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
                new Todo(ID : 2, Title: "Buy Unicorn"),
            };
            repo.StoreTodos(todos);
            var handler = new SelectTodosQueryHandler(repo);

            var result = handler.Handle(new SelectTodosQuery());

            Todo[] expected = {
                new Todo(ID : 1, Title: "Taste JavaScript", IsCompleted: true),
                new Todo(ID : 2, Title: "Buy Unicorn"),
            };
            Assert.That(result.Todos, Is.EqualTo(expected));
        }
    }
}
