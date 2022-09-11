using NUnit.Framework;
using Todos.Backend.Adapters;
using Todos.Backend.MessageHandlers;
using Todos.Contract.Data;
using Todos.Contract.Messages;

namespace Todos.Tests
{
    public class ClearCompletedCommandHandlerTests
    {
        [Test]
        public void ClearCompleted_RemovesCompletedTodos()
        {
            Todo[] todos = {
                new Todo(1, "Taste JavaScript", true),
                new Todo(2, "Buy Unicorn"),
            };
            var repo = new MemoryTodosRepository(todos);
            var handler = new ClearCompletedCommandHandler(repo);

            var status = handler.Handle(new ClearCompletedCommand());

            Assert.That(status, Is.EqualTo(new Success()));
            Todo[] expected = {
                new Todo(2, "Buy Unicorn"),
            };
            Assert.That(repo.LoadTodos(), Is.EqualTo(expected));
        }
    }
}
