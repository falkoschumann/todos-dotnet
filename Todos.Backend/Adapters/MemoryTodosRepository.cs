using System;
using System.Collections.Generic;
using Todos.Contract;
using Todos.Contract.Data;

namespace Todos.Backend.Adapters
{
    public class MemoryTodosRepository : ITodosRepository
    {
        private IReadOnlyList<Todo> _todos = Array.Empty<Todo>();

        public IReadOnlyList<Todo> LoadTodos()
        {
            return _todos;
        }

        public void StoreTodos(IReadOnlyList<Todo> todos)
        {
            _todos = todos;
        }
    }
}
