using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Todos.Contract.Data;

namespace Todos.Frontend
{
    /// <summary>
    /// Interaktionslogik für TodoList.xaml
    /// </summary>
    public partial class TodoList : UserControl
    {
        public event Action<bool> OnToggleAll;
        public event Action<int> OnToggle;
        public event Action<int> OnDestroy;
        public event Action<int, string> OnSave;

        public TodoList()
        {
            InitializeComponent();
        }

        public List<Todo> Todos
        {
            set { todos.ItemsSource = value; }
        }

        public void updateToggleAll(int activeCount, int completedCount)
        {
            if (activeCount > 0 && completedCount > 0)
            {
                toggleAll.IsChecked = null;
            } else
            {
                toggleAll.IsChecked = activeCount == 0;
            }
        }

        private void HandleToggleAll(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var isChecked = checkBox.IsChecked ?? false;
            OnToggleAll(isChecked);
        }

        private void HandleToggle(object sender, RoutedEventArgs e)
        {
            var todo = GetTodo(sender);
            OnToggle(todo.Id);
        }

        private void HandleDestroy(object sender, RoutedEventArgs e)
        {
            var todo = GetTodo(sender);
            OnDestroy(todo.Id);
        }

        private void HandleEdit(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount != 2)
            {
                return;
            }

            var todo = GetTodo(sender);
            var (view, edit, text) = GetControls(sender);
            view.Visibility = Visibility.Collapsed;
            edit.Visibility = Visibility.Visible;
            text.SelectAll();
            Action focusNewTodo = delegate { text.Focus(); };
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, focusNewTodo);
        }
        private void HandleSubmit(object sender, RoutedEventArgs e)
        {
            var todo = GetTodo(sender);
            var control = (TextBox)sender;
            OnSave(todo.Id, control.Text);
            var (view, edit, _) = GetControls(sender);
            view.Visibility = Visibility.Visible;
            edit.Visibility = Visibility.Collapsed;
        }

        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
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
    }
}
