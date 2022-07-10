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
            var repo = new MemoryTodosRepository();
            Todo[] todos = {
                new Todo(Id : 1, Title: "Taste JavaScript", IsCompleted: true),
                new Todo(Id : 2, Title: "Buy Unicorn", IsCompleted: false),
            };
            repo.StoreTodos(todos);
            var handler = new ToggleAllCommandHandler(repo);

            var status = handler.Handle(new ToggleAllCommand(IsCompleted: false));

            Assert.That(status, Is.EqualTo(new Success()));
            Todo[] expected = {
                new Todo(Id : 1, Title: "Taste JavaScript", IsCompleted: false),
                new Todo(Id : 2, Title: "Buy Unicorn", IsCompleted: false),
            };
            Assert.That(repo.LoadTodos(), Is.EqualTo(expected));
        }

        [Test]
        public void ToggleAll_SetAllTodosCompleted()
        {
            var repo = new MemoryTodosRepository();
            Todo[] todos = {
                new Todo(Id : 1, Title: "Taste JavaScript", IsCompleted: true),
                new Todo(Id : 2, Title: "Buy Unicorn", IsCompleted: false),
            };
            repo.StoreTodos(todos);
            var handler = new ToggleAllCommandHandler(repo);

            var status = handler.Handle(new ToggleAllCommand(IsCompleted: true));

            Assert.That(status, Is.EqualTo(new Success()));
            Todo[] expected = {
                new Todo(Id : 1, Title: "Taste JavaScript", IsCompleted: true),
                new Todo(Id : 2, Title: "Buy Unicorn", IsCompleted: true),
            };
            Assert.That(repo.LoadTodos(), Is.EqualTo(expected));
        }
    }
}
