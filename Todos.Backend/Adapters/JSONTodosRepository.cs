using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Todos.Contract;
using Todos.Contract.Data;

namespace Todos.Backend.Adapters
{
    public class JSONTodosRepository : ITodosRepository
    {
        private readonly string _filename;

        public JSONTodosRepository(string filename)
        {
            _filename = filename;
        }

        public IReadOnlyList<Todo> LoadTodos()
        {
            if (!File.Exists(_filename))
            {
                return Array.Empty<Todo>();
            }

            var json = File.ReadAllText(_filename);
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var dtos = JsonSerializer.Deserialize<TodoDTO[]>(json, options);
            return dtos.ToList()
                .Select(t => new Todo(t.Id, t.Title, t.IsCompleted))
                .ToArray();
        }

        public void StoreTodos(IReadOnlyList<Todo> todos)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var dtos = todos.ToList()
                .Select(t => new TodoDTO(t.Id, t.Title, t.IsCompleted))
                .ToArray();
            var json = JsonSerializer.Serialize(dtos, options);
            File.WriteAllText(_filename, json);
        }
    }

    internal struct TodoDTO
    {
        public TodoDTO(int id, string title, bool isCompleted = false)
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
        public bool IsCompleted { get; set; }
    }
}
