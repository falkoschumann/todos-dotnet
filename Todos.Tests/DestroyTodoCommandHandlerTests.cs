using Todos.Backend.Adapters;
using Todos.Backend.MessageHandlers;
using Todos.Contract.Data;
using Todos.Contract.Messages;

namespace Todos.Tests
{
    public class DestroyTodoCommandHandlerTests
    {
        [Test]
        public void DestryTodo_DestroysATodo()
        {
            var repo = new MemoryTodosRepository();
            Todo[] todos = {
                new Todo(ID : 1, Title: "Taste JavaScript", IsCompleted: true),
                new Todo(ID : 2, Title: "Buy Unicorn", IsCompleted: false),
            };
            repo.StoreTodos(todos);
            var handler = new DestroyTodoCommandHandler(repo);

            var status = handler.Handle(new DestroyTodoCommand(ID: 1));

            Assert.That(status, Is.EqualTo(new Success()));
            Todo[] expected = {
                new Todo(ID : 2, Title: "Buy Unicorn", IsCompleted: false),
            };
            Assert.That(repo.LoadTodos(), Is.EqualTo(expected));
        }
    }
}
