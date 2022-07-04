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

        private Todo[] todos;
        private long lastTicks = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnActivated(EventArgs e)
        {
            OnSelectTodosQuery(new SelectTodosQuery());
        }

        public void Display(SelectTodosQueryResult result)
        {
            todos = result.Todos;
            todoList.ItemsSource = result.Todos;
        }

        private void HandleToggleAll(object sender, RoutedEventArgs e)
        {
            var control = (CheckBox)sender;
            OnToggleAllCommand(new ToggleAllCommand(control.IsChecked ?? false));
        }

        private void HandleNewTodoKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
            {
                return;
            }

            var control = (TextBox)sender;
            var title = control.Text.Trim();
            if (title.Length == 0)
            {
                return;
            }

            OnAddTodoCommand(new AddTodoCommand(title));
        }

        private void HandleToggleTodo(object sender, RoutedEventArgs e)
        {
            var control = (CheckBox)sender;
            OnToggleTodoCommand(new ToggleTodoCommand((int)control.Tag));
        }

        private void HandleEditTodo(object sender, MouseButtonEventArgs e)
        {
            if ((DateTime.Now.Ticks - lastTicks) < 3000000)
            {
                ShowTodoEditor(sender);
            }
            lastTicks = DateTime.Now.Ticks;
        }

        private void HandleDestroyTodo(object sender, RoutedEventArgs e)
        {
            var control = (Button)sender;
            OnDestroyTodoCommand(new DestroyTodoCommand((int)control.Tag));
        }

        private void HandleEditTodoKeyDown(object sender, KeyEventArgs e) {
            switch (e.Key)
            {
                case Key.Enter:
                    HandleSubmitEditTodo(sender, e);
                    break;
                case Key.Escape:
                    var control = (TextBox)sender;
                    var id = (int)control.Tag;
                    int idx = -1;
                    for (var i = 0; i < todos.Length; i++)
                    {
                        if (todos[i].ID == id)
                        {
                            idx = i;
                            break;
                        }
                    }
                    var todoItem = (ListBoxItem) todoList.ItemContainerGenerator.ContainerFromIndex(idx);
                    var todo = (Todo)todoItem.Content;
                    control.Text = todo.Title;
                    ShowTodoViewer(sender);
                    break;
            }
        }

        private void HandleSubmitEditTodo(object sender, RoutedEventArgs e) {
            return;

            var control = (TextBox)sender;
            var value = control.Text.Trim();
            if (value != "")
            {
                OnSaveTodoCommand(new SaveTodoCommand((int)control.Tag, value));
                control.Text = "";
            } else
            {
                OnDestroyTodoCommand(new DestroyTodoCommand((int)control.Tag));
            }

            ShowTodoViewer(sender);
        }

        private void ShowTodoEditor(object sender)
        {
            // TODO: Close any other editor

            var text = (FrameworkElement)sender;
            var viewerGrid = (Grid)text.Parent;
            var stack = (StackPanel)viewerGrid.Parent;

            // Hide viewer
            stack.Children[0].Visibility = Visibility.Collapsed;
            
            // Show editor
            var editorGrid = (Grid)stack.Children[1];
            editorGrid.Visibility = Visibility.Visible;
            var editorTextBox = (TextBox)editorGrid.Children[0];
            // FIXME: Text is not selected and focused
            Debug.WriteLine("TextBox: " + editorTextBox.Text);
            editorTextBox.SelectAll();
            editorTextBox.Focus();
        }

        private void ShowTodoViewer(object sender)
        {
            var text = (FrameworkElement)sender;
            var grid = (Grid)text.Parent;
            var stack = (StackPanel)grid.Parent;

            // Show viewer
            stack.Children[0].Visibility = Visibility.Visible;

            // Hide editor
            stack.Children[1].Visibility = Visibility.Collapsed;
        }
    }
}
