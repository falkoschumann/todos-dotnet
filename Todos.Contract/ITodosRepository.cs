using System.Collections.Generic;
using Todos.Contract.Data;

namespace Todos.Contract
{
    public interface ITodosRepository
    {
        IReadOnlyList<Todo> LoadTodos();

        void StoreTodos(IEnumerable<Todo> todos);
    }
}
