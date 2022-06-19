using Todos.Contract.Data;

namespace Todos.Contract.Messages
{
    public readonly record struct SelectTodosQuery();

    public readonly record struct SelectTodosQueryResult(Todo[] Todos);

    public interface ISelectTodosQueryHandling
    {
        SelectTodosQueryResult Handle(SelectTodosQuery query);
    }
}
