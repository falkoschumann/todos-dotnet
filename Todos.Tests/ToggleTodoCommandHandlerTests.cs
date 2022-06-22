using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                new Todo(ID : 1, Title: "Taste JavaScript", IsCompleted: true),
                new Todo(ID : 2, Title: "Buy Unicorn"),
            };
            repo.StoreTodos(todos);
            var handler = new ToggleTodoCommandHandler(repo);

            var status = handler.Handle(new ToggleTodoCommand(ID: 1));

            Assert.That(status, Is.EqualTo(new Success()));
            Todo[] expected = {
                new Todo(ID : 1, Title: "Taste JavaScript", IsCompleted: false),
                new Todo(ID : 2, Title: "Buy Unicorn"),
            };
            Assert.That(repo.LoadTodos(), Is.EqualTo(expected));
        }
    }
}
