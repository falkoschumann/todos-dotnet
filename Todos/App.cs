using System;
using System.Diagnostics;
using System.IO;
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
            /*
            var repo = new MemoryTodosRepository();
            Todo[] todos = {
                new Todo(1, "Taste JavaScript", isCompleted: true),
                new Todo(2, "Buy Unicorn"),
            };
            repo.StoreTodos(todos);
            repo.StoreTodos(todos);
            */
            var dataDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Muspellheim",
                "Todos"
            );
            Directory.CreateDirectory(dataDir);
            var dataFile = Path.Combine(dataDir, "todos.json");
            var repo = new JSONTodosRepository(dataFile);
            //var repo = new CSVTodosRepository("todos.csv");

            var addTodoCommandHandler = new AddTodoCommandHandler(repo);
            var clearCompletedCommandHandler = new ClearCompletedCommandHandler(repo);
            var destroyTodoCommandHandler = new DestroyTodoCommandHandler(repo);
            var saveTodoCommandHandler = new SaveTodoCommandHandler(repo);
            var toggleAllCommandHandler = new ToggleAllCommandHandler(repo);
            var toggleTodoCommandHandler = new ToggleTodoCommandHandler(repo);
            var selectTodosQueryHandler = new SelectTodosQueryHandler(repo);

            var frontend = new MainWindow();
            frontend.OnAddTodoCommand += c =>
            {
                addTodoCommandHandler.Handle(c);
                var result = selectTodosQueryHandler.Handle(new SelectTodosQuery());
                frontend.Display(result);
            };
            frontend.OnClearCompletedCommand += c =>
            {
                clearCompletedCommandHandler.Handle(c);
                var result = selectTodosQueryHandler.Handle(new SelectTodosQuery());
                frontend.Display(result);
            };
            frontend.OnDestroyTodoCommand += c =>
            {
                destroyTodoCommandHandler.Handle(c);
                var result = selectTodosQueryHandler.Handle(new SelectTodosQuery());
                frontend.Display(result);
            };
            frontend.OnSaveTodoCommand += c =>
            {
                saveTodoCommandHandler.Handle(c);
                var result = selectTodosQueryHandler.Handle(new SelectTodosQuery());
                frontend.Display(result);
            };
            frontend.OnToggleAllCommand += c =>
            {
                toggleAllCommandHandler.Handle(c);
                var result = selectTodosQueryHandler.Handle(new SelectTodosQuery());
                frontend.Display(result);
            };
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
