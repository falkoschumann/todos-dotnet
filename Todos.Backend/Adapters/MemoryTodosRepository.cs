using System;
using Todos.Contract;
using Todos.Contract.Data;

namespace Todos.Backend.Adapters
{
    public class MemoryTodosRepository : ITodosRepository
    {
        private Todo[] _todos = Array.Empty<Todo>();

        public Todo[] LoadTodos()
        {
            return _todos;
        }

        public void StoreTodos(Todo[] todos)
        {
            _todos = todos;
        }
    }
}
