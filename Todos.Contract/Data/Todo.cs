using System.Text.Json.Serialization;

namespace Todos.Contract.Data
{
    public record class Todo
    {
        public Todo() : this(0, "") { }

        public Todo(int id, string title, bool isCompleted = false)
        {
            Id = id;
            Title = title;
            IsCompleted = isCompleted;
        }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("completed")]
        public bool IsCompleted { get; set; } = false;
    };
}
