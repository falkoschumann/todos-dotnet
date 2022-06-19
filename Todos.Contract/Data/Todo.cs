namespace Todos.Contract.Data
{
    public readonly record struct Todo(int Id, string Title, bool Completed = false);
}
