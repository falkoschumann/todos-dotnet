using CsvHelper;
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
            using (var reader = new StreamReader(filename))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Todo>();
                return records.ToArray();
            }
        }

        public void StoreTodos(Todo[] todos)
        {
            using (var writer = new StreamWriter(filename))
            using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(todos);
            }
        }
    }
}
