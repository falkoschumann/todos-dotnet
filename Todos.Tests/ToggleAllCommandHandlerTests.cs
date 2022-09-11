using NUnit.Framework;
using Todos.Backend.Adapters;
using Todos.Backend.MessageHandlers;
using Todos.Contract.Data;
using Todos.Contract.Messages;

namespace Todos.Tests
{
    public class ToggleAllCommandHandlerTests
    {
        [Test]
        public void ToggleAll_SetAllTodosActive()
        {
            Todo[] todos = {
                new Todo(1, "Taste JavaScript", true),
                new Todo(2, "Buy Unicorn"),
            };
            var repo = new MemoryTodosRepository(todos);
            var handler = new ToggleAllCommandHandler(repo);

            var status = handler.Handle(new ToggleAllCommand(isCompleted: false));

            Assert.That(status, Is.EqualTo(new Success()));
            Todo[] expected = {
                new Todo(1, "Taste JavaScript", false),
                new Todo(2, "Buy Unicorn"),
            };
            Assert.That(repo.LoadTodos(), Is.EqualTo(expected));
        }

        [Test]
        public void ToggleAll_SetAllTodosCompleted()
        {
            Todo[] todos = {
                new Todo(1, "Taste JavaScript", true),
                new Todo(2, "Buy Unicorn"),
            };
            var repo = new MemoryTodosRepository(todos);
            var handler = new ToggleAllCommandHandler(repo);

            var status = handler.Handle(new ToggleAllCommand(isCompleted: true));

            Assert.That(status, Is.EqualTo(new Success()));
            Todo[] expected = {
                new Todo(1, "Taste JavaScript", true),
                new Todo(2, "Buy Unicorn", true),
            };
            Assert.That(repo.LoadTodos(), Is.EqualTo(expected));
        }
    }
}
