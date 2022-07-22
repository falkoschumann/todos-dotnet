using NUnit.Framework;
using Todos.Backend.Adapters;
using Todos.Backend.MessageHandlers;
using Todos.Contract.Data;
using Todos.Contract.Messages;

namespace Todos.Tests
{
    public class ToggleTodoCommandHandlerTests
    {
        [Test]
        public void ToggleTodo_ActivatesATodo()
        {
            var repo = new MemoryTodosRepository();
            Todo[] todos = {
                new Todo(1, "Taste JavaScript", true),
                new Todo(2, "Buy Unicorn"),
            };
            repo.StoreTodos(todos);
            var handler = new ToggleTodoCommandHandler(repo);

            var status = handler.Handle(new ToggleTodoCommand(1));

            Assert.That(status, Is.EqualTo(new Success()));
            Todo[] expected = {
                new Todo(1, "Taste JavaScript"),
                new Todo(2, "Buy Unicorn"),
            };
            Assert.That(repo.LoadTodos(), Is.EqualTo(expected));
        }

        [Test]
        public void ToggleTodo_CompletesATodo()
        {
            var repo = new MemoryTodosRepository();
            Todo[] todos = {
                new Todo(1, "Taste JavaScript", true),
                new Todo(2, "Buy Unicorn"),
            };
            repo.StoreTodos(todos);
            var handler = new ToggleTodoCommandHandler(repo);

            var status = handler.Handle(new ToggleTodoCommand(2));

            Assert.That(status, Is.EqualTo(new Success()));
            Todo[] expected = {
                new Todo(1, "Taste JavaScript", true),
                new Todo(2, "Buy Unicorn", true),
            };
            Assert.That(repo.LoadTodos(), Is.EqualTo(expected));
        }
    }
}
