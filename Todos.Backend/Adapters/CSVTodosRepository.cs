using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Todos.Contract;
using Todos.Contract.Data;

namespace Todos.Backend.Adapters
{
    public class CSVTodosRepository : ITodosRepository
    {
        private readonly string _filename;

        public CSVTodosRepository(string filename)
        {
            _filename = filename;
        }

        public IReadOnlyList<Todo> LoadTodos()
        {
            if (!File.Exists(_filename))
            {
                return Array.Empty<Todo>();
            }

            using (var reader = new StreamReader(_filename))
            {
                using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<TodoMap>();
                    var records = csv.GetRecords<Todo>();
                    return records.ToArray();
                }
            }
        }

        public void StoreTodos(IEnumerable<Todo> todos)
        {
            using (var writer = new StreamWriter(_filename))
            {
                using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<TodoMap>();
                    csv.WriteRecords(todos);
                }
            }
        }
    }

    public class TodoMap : ClassMap<Todo>
    {
        public TodoMap()
        {
            Map(t => t.Id).Name("id");
            Map(t => t.Title).Name("title");
            Map(t => t.IsCompleted).Name("completed");
        }
    }
}
