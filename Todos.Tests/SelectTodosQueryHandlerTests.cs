using NUnit.Framework;
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
            Todo[] todos = {
                new Todo(1, "Taste JavaScript", true),
                new Todo(2, "Buy Unicorn"),
            };
            var repo = new MemoryTodosRepository(todos);
            var handler = new SelectTodosQueryHandler(repo);

            var result = handler.Handle(new SelectTodosQuery());

            Todo[] expected = {
                new Todo(1, "Taste JavaScript", true),
                new Todo(2, "Buy Unicorn"),
            };
            Assert.That(result.Todos, Is.EqualTo(expected));
        }
    }
}
