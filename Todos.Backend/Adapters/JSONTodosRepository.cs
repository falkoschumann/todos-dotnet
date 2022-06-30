﻿using System.Text.Json;
using Todos.Contract;
using Todos.Contract.Data;

namespace Todos.Backend.Adapters
{
    public class JSONTodosRepository : ITodosRepository
    {
        private readonly string filename;

        public JSONTodosRepository(string filename)
        {
            this.filename = filename;
        }

        public Todo[] LoadTodos()
        {
            var json = File.ReadAllText(this.filename);
            return JsonSerializer.Deserialize<Todo[]>(json)!;
        }

        public void StoreTodos(Todo[] todos)
        {
            var json = JsonSerializer.Serialize(todos);
            File.WriteAllText(this.filename, json);
        }
    }
}