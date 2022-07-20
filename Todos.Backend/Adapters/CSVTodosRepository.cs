using CsvHelper;
using CsvHelper.Configuration;
using Todos.Contract;
using Todos.Contract.Data;

namespace Todos.Backend.Adapters
{
    public class CSVTodosRepository : ITodosRepository
    {
        private readonly string filename;

        public CSVTodosRepository(string filename)
        {
            this.filename = filename;
        }

        public Todo[] LoadTodos()
        {
            if (!File.Exists(this.filename))
            {
                return Array.Empty<Todo>();
            }

            using var reader = new StreamReader(filename);
            using var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<TodoMap>();
            var records = csv.GetRecords<Todo>();
            return records.ToArray();
        }

        public void StoreTodos(Todo[] todos)
        {
            using var writer = new StreamWriter(filename);
            using var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<TodoMap>();
            csv.WriteRecords(todos);
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
