using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todos.Contract;
using Todos.Contract.Data;

namespace Todos.Backend.Adapters
{
    public class MemoryTodosRepository : ITodosRepository
    {
        private Todo[] todos = Array.Empty<Todo>();

        public Todo[] LoadTodos()
        {
            return todos;
        }

        public void StoreTodos(Todo[] todos)
        {
            this.todos = todos;
        }
    }
}
