using Todos.Backend.Adapters;
using Todos.Backend.MessageHandler;
using Todos.Contract.Data;
using Todos.Contract.Messages;

namespace Todos.Tests
{
    public class SelectTodosQueryTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SelectTodos_ReturnsAllTodos()
        {
            var repo = new MemoryTodosRepository();
            Todo[] todos = {
                new Todo(Id : 1, Title: "Taste JavaScript", Completed: true),
                new Todo(Id : 2, Title: "Buy Unicorn"),
            };
            repo.StoreTodos(todos);
            var handler = new SelectTodosQueryHandler(repo);

            var result = handler.Handle(new SelectTodosQuery());

            Assert.That(result, Is.EqualTo(new SelectTodosQueryResult(todos)));
        }
    }
}
