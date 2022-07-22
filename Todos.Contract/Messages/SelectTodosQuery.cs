using Todos.Contract.Data;

namespace Todos.Contract.Messages
{
    public readonly struct SelectTodosQuery { }

    public readonly struct SelectTodosQueryResult
    {
        public SelectTodosQueryResult(Todo[] todos)
        {
            Todos = todos;
        }

        public Todo[] Todos { get; }
    }

    public interface ISelectTodosQueryHandling
    {
        SelectTodosQueryResult Handle(SelectTodosQuery query);
    }
}
