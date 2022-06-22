namespace Todos.Contract.Data
{
    public readonly record struct Todo(int ID, string Title, bool IsCompleted = false);
}
