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
                new Todo(Id : 1, Title: "Taste JavaScript", IsCompleted: true),
                new Todo(Id : 2, Title: "Buy Unicorn"),
            };
            repo.StoreTodos(todos);

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
