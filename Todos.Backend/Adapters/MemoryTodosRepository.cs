using System;
using System.Collections.Generic;
using Todos.Contract;
using Todos.Contract.Data;

namespace Todos.Backend.Adapters
{
    public class MemoryTodosRepository : ITodosRepository
    {
        private IReadOnlyList<Todo> _todos;

        public MemoryTodosRepository() : this(Array.Empty<Todo>()) { }

        public MemoryTodosRepository(IEnumerable<Todo> todos)
        {
            StoreTodos(todos);
        }

        public IReadOnlyList<Todo> LoadTodos()
        {
            return _todos;
        }

        public void StoreTodos(IEnumerable<Todo> todos)
        {
            _todos = new List<Todo>(todos).AsReadOnly();
        }
    }
}
