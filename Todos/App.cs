using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Todos.Backend.Adapters;
using Todos.Backend.MessageHandlers;
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
                new Todo(ID : 1, Title: "Taste JavaScript", IsCompleted: true),
                new Todo(ID : 2, Title: "Buy Unicorn"),
            };
            repo.StoreTodos(todos);

            var selectTodosQueryHandler = new SelectTodosQueryHandler(repo);
            var toggleTodoCommandHandler = new ToggleTodoCommandHandler(repo);

            var frontend = new MainWindow();
            frontend.OnToggleTodoCommand += c =>
            {
                toggleTodoCommandHandler.Handle(c);
                var result = selectTodosQueryHandler.Handle(new SelectTodosQuery());
                frontend.Display(result);
            };
            frontend.OnSelectTodosQuery += q =>
            {
                var result = selectTodosQueryHandler.Handle(new SelectTodosQuery());
                frontend.Display(result);
            };

            var app = new App();
            app.Run(frontend);
        }
    }
}
