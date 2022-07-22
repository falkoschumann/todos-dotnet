namespace Todos.Contract.Data
{
    public struct Todo
    {
        public Todo(int id = 0, string title = "", bool isCompleted = false)
        {
            Id = id;
            Title = title;
            IsCompleted = isCompleted;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsCompleted { get; set; }
    };
}
