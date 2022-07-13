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
            var todos = Array.Empty<Todo>();
            repo.StoreTodos(todos);
            var handler = new AddTodoCommandHandler(repo);

            var status = handler.Handle(new AddTodoCommand(Title: "Taste JavaScript"));

            Assert.That(status, Is.EqualTo(new Success()));
            Todo[] expected = {
                new Todo(Id : 1, Title: "Taste JavaScript", IsCompleted: false),
            };
            Assert.That(repo.LoadTodos(), Is.EqualTo(expected));
        }

        public void AddTodo_IncrementsId()
        {
            var repo = new MemoryTodosRepository();
            Todo[] todos = {
                new Todo(Id : 1, Title: "Taste JavaScript", IsCompleted: true),
            };
            repo.StoreTodos(todos);
            var handler = new AddTodoCommandHandler(repo);

            var status = handler.Handle(new AddTodoCommand(Title: "Buy Unicorn"));

            Assert.That(status, Is.EqualTo(new Success()));
            Todo[] expected = {
                new Todo(Id : 1, Title: "Taste JavaScript", IsCompleted: true),
                new Todo(Id : 2, Title: "Buy Unicorn", IsCompleted: false),
            };
            Assert.That(repo.LoadTodos(), Is.EqualTo(expected));
        }

        [Test]
        public void AddTodo_DoesNothingIfTitleIsEmpty()
        {
            var repo = new MemoryTodosRepository();
            Todo[] todos = {
                new Todo(Id : 1, Title: "Taste JavaScript", IsCompleted: true),
            };
            repo.StoreTodos(todos);
            var handler = new AddTodoCommandHandler(repo);

            var status = handler.Handle(new AddTodoCommand(Title: ""));

            Assert.That(status, Is.EqualTo(new Success()));
            Todo[] expected = {
                new Todo(Id : 1, Title: "Taste JavaScript", IsCompleted: true),
            };
            Assert.That(repo.LoadTodos(), Is.EqualTo(expected));
        }
    }
}
