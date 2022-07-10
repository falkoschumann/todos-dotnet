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
            var repo = new MemoryTodosRepository();
            Todo[] todos = {
                new Todo(Id : 1, Title: "Taste JavaScript", IsCompleted: true),
                new Todo(Id : 2, Title: "Buy Unicorn", IsCompleted: false),
            };
            repo.StoreTodos(todos);
            var handler = new SaveTodoCommandHandler(repo);

            var status = handler.Handle(new SaveTodoCommand(Id: 1, Title: "Taste TypeScript"));

            Assert.That(status, Is.EqualTo(new Success()));
            Todo[] expected = {
                new Todo(Id : 1, Title: "Taste TypeScript", IsCompleted: true),
                new Todo(Id : 2, Title: "Buy Unicorn", IsCompleted: false),
            };
            Assert.That(repo.LoadTodos(), Is.EqualTo(expected));
        }

        [Test]
        public void SaveTodo_DestroyTodoIfTitleIsEmpty()
        {
            var repo = new MemoryTodosRepository();
            Todo[] todos = {
                new Todo(Id : 1, Title: "Taste JavaScript", IsCompleted: true),
                new Todo(Id : 2, Title: "Buy Unicorn", IsCompleted: false),
            };
            repo.StoreTodos(todos);
            var handler = new SaveTodoCommandHandler(repo);

            var status = handler.Handle(new SaveTodoCommand(Id: 1, Title: ""));

            Assert.That(status, Is.EqualTo(new Success()));
            Todo[] expected = {
                new Todo(Id : 2, Title: "Buy Unicorn", IsCompleted: false),
            };
            Assert.That(repo.LoadTodos(), Is.EqualTo(expected));
        }
    }
}
