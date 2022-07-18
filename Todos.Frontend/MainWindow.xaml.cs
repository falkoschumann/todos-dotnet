using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Todos.Contract.Data;
using Todos.Contract.Messages;

namespace Todos.Frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // TODO: Main and Footer are invisble, when no todos exist

        public event Action<AddTodoCommand>? OnAddTodoCommand;
        public event Action<ClearCompletedCommand>? OnClearCompletedCommand;
        public event Action<DestroyTodoCommand>? OnDestroyTodoCommand;
        public event Action<SaveTodoCommand>? OnSaveTodoCommand;
        public event Action<ToggleAllCommand>? OnToggleAllCommand;
        public event Action<ToggleTodoCommand>? OnToggleTodoCommand;
        public event Action<SelectTodosQuery>? OnSelectTodosQuery;

        public MainWindow()
        {
            InitializeComponent();
            header.OnAddTodo += t => OnAddTodoCommand?.Invoke(new AddTodoCommand(t));
            todoList.OnToggleAll += c => OnToggleAllCommand?.Invoke(new ToggleAllCommand(c));
            todoList.OnToggle += id => OnToggleTodoCommand?.Invoke(new ToggleTodoCommand(id));
            todoList.OnDestroy += id => OnDestroyTodoCommand?.Invoke(new DestroyTodoCommand(id));
            todoList.OnSave += (id, title) => OnSaveTodoCommand?.Invoke(new SaveTodoCommand(id, title));
            footer.OnClearCompleted += () => OnClearCompletedCommand?.Invoke(new ClearCompletedCommand());
            footer.OnFilterChanged += f => OnSelectTodosQuery?.Invoke(new SelectTodosQuery());
        }

        protected override void OnActivated(EventArgs e)
        {
            OnSelectTodosQuery?.Invoke(new SelectTodosQuery());
        }

        public void Display(SelectTodosQueryResult result)
        {
            var shownTodos = result.Todos.ToList().FindAll(t => footer.Filter switch
            {
                Filter.All => true,
                Filter.Active => !t.IsCompleted,
                Filter.Completed => t.IsCompleted,
                _ => false,
            });
            var activeCount = result.Todos.ToList().FindAll(t => !t.IsCompleted).Count;
            var completedCount = result.Todos.Length - activeCount;
            todoList.Todos = shownTodos;
            todoList.updateToggleAll(activeCount, completedCount);
            footer.ActiveCount = activeCount;
            footer.CompletedCount = completedCount;
        }
    }
}
