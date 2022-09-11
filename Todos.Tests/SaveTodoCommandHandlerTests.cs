using NUnit.Framework;
using Todos.Backend.Adapters;
using Todos.Backend.MessageHandlers;
using Todos.Contract.Data;
using Todos.Contract.Messages;

namespace Todos.Tests
{
    public class SaveTodoCommandHandlerTests
    {
        [Test]
        public void SaveTodo_ChangesTodosTitle()
        {
            Todo[] todos = {
                new Todo(1, "Taste JavaScript", true),
                new Todo(2, "Buy Unicorn"),
            };
            var repo = new MemoryTodosRepository(todos);
            var handler = new SaveTodoCommandHandler(repo);

            var status = handler.Handle(new SaveTodoCommand(1, "Taste TypeScript"));

            Assert.That(status, Is.EqualTo(new Success()));
            Todo[] expected = {
                new Todo(1, "Taste TypeScript", true),
                new Todo(2, "Buy Unicorn"),
            };
            Assert.That(repo.LoadTodos(), Is.EqualTo(expected));
        }

        [Test]
        public void SaveTodo_DestroyTodoIfTitleIsEmpty()
        {
            Todo[] todos = {
                new Todo(1, "Taste JavaScript", true),
                new Todo(2, "Buy Unicorn"),
            };
            var repo = new MemoryTodosRepository(todos);
            var handler = new SaveTodoCommandHandler(repo);

            var status = handler.Handle(new SaveTodoCommand(1, ""));

            Assert.That(status, Is.EqualTo(new Success()));
            Todo[] expected = {
                new Todo(2, "Buy Unicorn"),
            };
            Assert.That(repo.LoadTodos(), Is.EqualTo(expected));
        }
    }
}
