namespace Todos.Contract.Data
{
    public readonly struct Todo
    {
        public Todo(int id = 0, string title = "", bool isCompleted = false)
        {
            Id = id;
            Title = title;
            IsCompleted = isCompleted;
        }

        public int Id { get; }

        public string Title { get; }

        public bool IsCompleted { get; }
    }
}
