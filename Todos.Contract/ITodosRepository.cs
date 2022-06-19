using Todos.Contract.Data;

namespace Todos.Contract
{
    public interface ITodosRepository
    {
        Todo[] LoadTodos();
        void StoreTodos(Todo[] todos);
    }
}
