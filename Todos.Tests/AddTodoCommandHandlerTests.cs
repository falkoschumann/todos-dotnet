using NUnit.Framework;
using Todos.Backend.Adapters;
using Todos.Backend.MessageHandlers;
using Todos.Contract.Data;
using Todos.Contract.Messages;

namespace Todos.Tests
{
    public class AddTodoCommandHandlerTests
    {
        [Test]
        public void AddTodo_SavesNewTodo()
        {
            var repo = new MemoryTodosRepository();
            var handler = new AddTodoCommandHandler(repo);

            var status = handler.Handle(new AddTodoCommand("Taste JavaScript"));

            Assert.That(status, Is.EqualTo(new Success()));
            Todo[] expected = {
                new Todo(1, "Taste JavaScript"),
            };
            Assert.That(repo.LoadTodos(), Is.EqualTo(expected));
        }

        public void AddTodo_IncrementsId()
        {
            Todo[] todos = {
                new Todo(1, "Taste JavaScript", true),
            };
            var repo = new MemoryTodosRepository(todos);
            var handler = new AddTodoCommandHandler(repo);

            var status = handler.Handle(new AddTodoCommand("Buy Unicorn"));

            Assert.That(status, Is.EqualTo(new Success()));
            Todo[] expected = {
                new Todo(1, "Taste JavaScript", true),
                new Todo(2, "Buy Unicorn"),
            };
            Assert.That(repo.LoadTodos(), Is.EqualTo(expected));
        }

        [Test]
        public void AddTodo_DoesNothingIfTitleIsEmpty()
        {
            Todo[] todos = {
                new Todo(1, "Taste JavaScript", true),
            };
            var repo = new MemoryTodosRepository(todos);
            var handler = new AddTodoCommandHandler(repo);

            var status = handler.Handle(new AddTodoCommand(""));

            Assert.That(status, Is.EqualTo(new Success()));
            Todo[] expected = {
                new Todo(1, "Taste JavaScript", true),
            };
            Assert.That(repo.LoadTodos(), Is.EqualTo(expected));
        }
    }
}
