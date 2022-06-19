using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Todos.Backend.Adapters;
using Todos.Backend.MessageHandler;
using Todos.Contract.Data;
using Todos.Contract.Messages;
using Todos.Frontend;

namespace Todos
{
    public class App : Application
    {
        [STAThread]
        public static void Main()
        {
            var repo = new MemoryTodosRepository();
            Todo[] todos = {
                new Todo(Id : 1, Title: "Taste JavaScript", Completed: true),
                new Todo(Id : 2, Title: "Buy Unicorn"),
            };
            repo.StoreTodos(todos);

            var handler = new SelectTodosQueryHandler(repo);

            var frontend = new MainWindow();
            frontend.OnSelectTodosQuery += q =>
            {
                var result = handler.Handle(new SelectTodosQuery());
                frontend.Display(result);
            };

            var app = new App();
            app.Run(frontend);
        }
    }
}
