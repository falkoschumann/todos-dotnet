using System.Collections.Generic;
using Todos.Contract.Data;

namespace Todos.Contract.Messages
{
    public readonly struct SelectTodosQuery { }

    public readonly struct SelectTodosQueryResult
    {
        public SelectTodosQueryResult(IReadOnlyList<Todo> todos)
        {
            Todos = todos;
        }

        public IReadOnlyList<Todo> Todos { get; }
    }

    public interface ISelectTodosQueryHandling
    {
        SelectTodosQueryResult Handle(SelectTodosQuery query);
    }
}
