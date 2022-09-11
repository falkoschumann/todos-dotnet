using System;
using System.Linq;
using System.Windows;
using Todos.Contract.Messages;

namespace Todos.Frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public event Action<AddTodoCommand> OnAddTodoCommand;
        public event Action<ClearCompletedCommand> OnClearCompletedCommand;
        public event Action<DestroyTodoCommand> OnDestroyTodoCommand;
        public event Action<SaveTodoCommand> OnSaveTodoCommand;
        public event Action<ToggleAllCommand> OnToggleAllCommand;
        public event Action<ToggleTodoCommand> OnToggleTodoCommand;
        public event Action<SelectTodosQuery> OnSelectTodosQuery;

        public MainWindow()
        {
            InitializeComponent();
            header.OnAddTodo += t => OnAddTodoCommand(new AddTodoCommand(t));
            todoList.OnToggleAll += c => OnToggleAllCommand(new ToggleAllCommand(c));
            todoList.OnToggle += id => OnToggleTodoCommand(new ToggleTodoCommand(id));
            todoList.OnDestroy += id => OnDestroyTodoCommand(new DestroyTodoCommand(id));
            todoList.OnSave += (id, title) => OnSaveTodoCommand(new SaveTodoCommand(id, title));
            footer.OnClearCompleted += () => OnClearCompletedCommand(new ClearCompletedCommand());
            footer.OnFilterChanged += f => OnSelectTodosQuery(new SelectTodosQuery());
        }

        protected override void OnActivated(EventArgs e)
        {
            header.FocusNewTodo();
            OnSelectTodosQuery(new SelectTodosQuery());
        }

        public void Display(SelectTodosQueryResult result)
        {
            var shownTodos = result.Todos.ToList().FindAll(t =>
            {
                switch (footer.Filter)
                {
                    case Filter.All:
                        return true;
                    case Filter.Active:
                        return !t.IsCompleted;
                    case Filter.Completed:
                        return t.IsCompleted;
                    default:
                        return false;
                }
            });
            var activeCount = result.Todos.ToList().FindAll(t => !t.IsCompleted).Count;
            var completedCount = result.Todos.Count - activeCount;
            todoList.Visibility = result.Todos.Count > 0 ? Visibility.Visible : Visibility.Hidden;
            todoList.Todos = shownTodos;
            todoList.updateToggleAll(activeCount, completedCount);
            footer.Visibility = result.Todos.Count > 0 ? Visibility.Visible : Visibility.Hidden;
            footer.ActiveCount = activeCount;
            footer.CompletedCount = completedCount;
        }
    }
}
