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
        public event Action<AddTodoCommand>? OnAddTodoCommand;
        public event Action<ClearCompletedCommand>? OnClearCompletedCommand;
        public event Action<DestroyTodoCommand>? OnDestroyTodoCommand;
        public event Action<SaveTodoCommand>? OnSaveTodoCommand;
        public event Action<ToggleAllCommand>? OnToggleAllCommand;
        public event Action<ToggleTodoCommand>? OnToggleTodoCommand;
        public event Action<SelectTodosQuery>? OnSelectTodosQuery;

        private Filter filter = Filter.All;

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnActivated(EventArgs e)
        {
            OnSelectTodosQuery?.Invoke(new SelectTodosQuery());
        }

        #region Messages

        public void Display(SelectTodosQueryResult result)
        {
            var shownTodos = result.Todos.ToList().FindAll(t => filter switch
            {
                Filter.All => true,
                Filter.Active => !t.IsCompleted,
                Filter.Completed => t.IsCompleted,
                _ => false,
            });
            todoList.ItemsSource = shownTodos;
            toggleAll.IsChecked = result.Todos.Select(t => t.IsCompleted).Aggregate(false, (e1, e2) => e1 && e2);
            var activeCount = result.Todos.ToList().FindAll(t => !t.IsCompleted).Count();
            this.activeCount.Text = $"{activeCount} {pluralize(activeCount, "item")} left";
            var completedCount = result.Todos.Length - activeCount;
            clearCompleted.Visibility = completedCount > 0 ? Visibility.Visible : Visibility.Hidden;
        }

        private static string pluralize(int count, string word)
        {
            return count > 1 ? $"{word}s" : word;
        }

        #endregion

        #region Header

        private void HandleNewTodoKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
            {
                return;
            }

            var textBox = (TextBox)sender;
            var title = textBox.Text.Trim();
            OnAddTodoCommand?.Invoke(new AddTodoCommand(title));
            textBox.Text = "";
        }

        #endregion

        #region Todo List

        private void HandleToggleAll(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var isChecked = checkBox.IsChecked ?? false;
            OnToggleAllCommand?.Invoke(new ToggleAllCommand(isChecked));
        }

        #endregion

        #region Todo Item

        private void HandleToggle(object sender, RoutedEventArgs e)
        {
            var todo = GetTodo(sender);
            OnToggleTodoCommand?.Invoke(new ToggleTodoCommand(todo.Id));
        }

        private void HandleDestroy(object sender, RoutedEventArgs e)
        {
            var todo = GetTodo(sender);
            OnDestroyTodoCommand?.Invoke(new DestroyTodoCommand(todo.Id));
        }

        private void HandleEdit(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount != 2)
            {
                return;
            }

            Debug.WriteLine("HandleEdit");
            var todo = GetTodo(sender);
            var (view, edit, text) = GetControls(sender);
            view.Visibility = Visibility.Collapsed;
            edit.Visibility = Visibility.Visible;
            text.SelectAll();
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, () =>
            {
                text.Focus();
            });
        }
        private void HandleSubmit(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("HandleSubmit");
            var todo = GetTodo(sender);
            var control = (TextBox)sender;
            OnSaveTodoCommand?.Invoke(new SaveTodoCommand(todo.Id, control.Text));
            var (view, edit, _) = GetControls(sender);
            view.Visibility = Visibility.Visible;
            edit.Visibility = Visibility.Collapsed;
        }

        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("HandleKeyDown");
            switch (e.Key)
            {
                case Key.Enter:
                    HandleSubmit(sender, e);
                    break;
                case Key.Escape:
                    HandleCancel(sender);
                    break;
            }
        }

        private static void HandleCancel(object sender)
        {
            var todo = GetTodo(sender);
            var (view, edit, text) = GetControls(sender);
            text.Text = todo.Title;
            view.Visibility = Visibility.Visible;
            edit.Visibility = Visibility.Collapsed;
        }

        private static Todo GetTodo(object sender)
        {
            var element = (FrameworkElement)sender;
            var parent = (FrameworkElement)element.Parent;
            var stack = (StackPanel)parent.Parent;
            var todo = (Todo)stack.Tag;
            return todo;
        }

        private static (FrameworkElement, FrameworkElement, TextBox) GetControls(object sender)
        {
            var element = (FrameworkElement)sender;
            var parent = (FrameworkElement)element.Parent;
            var stack = (StackPanel)parent.Parent;
            var view = (FrameworkElement)stack.Children[0];
            var edit = (FrameworkElement)stack.Children[1];
            var text = (TextBox)edit.FindName("text");
            return (view, edit, text);
        }

        #endregion

        #region Footer

        private void HandleFilterAll(object sender, RoutedEventArgs e)
        {
            filter = Filter.All;
            OnSelectTodosQuery?.Invoke(new SelectTodosQuery());
        }

        private void HandleFilterActive(object sender, RoutedEventArgs e)
        {
            filter = Filter.Active;
            OnSelectTodosQuery?.Invoke(new SelectTodosQuery());
        }

        private void HandleFilterCompleted(object sender, RoutedEventArgs e)
        {
            filter = Filter.Completed;
            OnSelectTodosQuery?.Invoke(new SelectTodosQuery());
        }

        private void HandleClearCompleted(object sender, RoutedEventArgs e)
        {
            OnClearCompletedCommand?.Invoke(new ClearCompletedCommand());
            OnSelectTodosQuery?.Invoke(new SelectTodosQuery());
        }

        #endregion
    }

    enum Filter { All, Active, Completed }
}
