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
            todoList.ItemsSource = result.Todos;
            toggleAll.IsChecked = result.Todos.Select(t => t.IsCompleted).Aggregate((e1, e2) => e1 && e2);
        }

        private void HandleToggleAllClick(object sender, RoutedEventArgs e)
        {
            var control = (CheckBox)sender;
            var isChecked = control.IsChecked ?? false;
            OnToggleAllCommand(new ToggleAllCommand(isChecked));
        }

        private void HandleNewTodoKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
            {
                return;
            }

            var control = (TextBox)sender;
            var title = control.Text.Trim();
            OnAddTodoCommand(new AddTodoCommand(title));
            control.Text = "";
        }

        private void HandleToggleTodoClick(object sender, RoutedEventArgs e)
        {
            var control = (CheckBox)sender;
            var id = (int)control.Tag;
            OnToggleTodoCommand(new ToggleTodoCommand(id));
        }

        private void HandleViewTodoClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount != 2)
            {
                return;
            }

            SetTodoEditVisible(sender, true);
        }

        private static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T t)
                {
                    return t;
                }else
                {
                    var children = FindVisualChild<T>(child);
                    if (children != null)
                    {
                        return children;
                    }
                }
            }
            return null;
        }

        private void HandleDestroyTodo(object sender, RoutedEventArgs e)
        {
            var control = (Button)sender;
            OnDestroyTodoCommand(new DestroyTodoCommand((int)control.Tag));
        }

        private void HandleEditTodoLostFocus(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("HandleEditTodoFocusLost: " + sender);
            var control = (TextBox)sender;
            var value = control.Text.Trim();
            if (value != "")
            {
                OnSaveTodoCommand(new SaveTodoCommand((int)control.Tag, value));
            }
            else
            {
                OnDestroyTodoCommand(new DestroyTodoCommand((int)control.Tag));
            }
        }

        private void HandleEditTodoKeyDown(object sender, KeyEventArgs e) {
            Debug.WriteLine("HandleEditTodoKeyDown: " + sender);
            switch (e.Key)
            {
                case Key.Enter:
                    HandleEditTodoLostFocus(sender, e);
                    break;
                case Key.Escape:
                    // FIXME: Ein geänderter Wert wird gesichert, statt den alten wiederherzustellen.
                    SetTodoEditVisible(sender, false);
                    OnSelectTodosQuery(new SelectTodosQuery());
                    break;
            }
        }

        private void SetTodoEditVisible(object sender, bool visible)
        {
            var control = (FrameworkElement)sender;
            var id = (int)control.Tag;
            var idx = -1;
            var todos = (Todo[])todoList.ItemsSource;
            for (var i = 0; i < todos.Length; i++)
            {
                var todo = todos[i];
                if (todo.ID == id)
                {
                    idx = i;
                    break;
                }
            }
            Debug.WriteLine("SetTodoEditVisible: idx=" + idx);

            //var item = todoList.ItemContainerGenerator.ContainerFromItem(todoList.Items.CurrentItem);
            var item = todoList.ItemContainerGenerator.ContainerFromIndex(idx);
            if (item == null)
            {
                Debug.WriteLine("SetTodoEditVisible: item=null");
                return;
            }

            Debug.WriteLine("SetTodoEditVisible: " + item);
            var presenter = FindVisualChild<ContentPresenter>(item);
            var template = presenter.ContentTemplate;
            var view = (Grid)template.FindName("todoView", presenter);
            var edit = (Grid)template.FindName("todoEdit", presenter);

            if (visible)
            {
                view.Visibility = Visibility.Collapsed;
                edit.Visibility = Visibility.Visible;
                var text = (TextBox)edit.Children[0];
                Debug.WriteLine("SetTodoEditVisible: text=" + text);
                text.SelectAll();
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, () => {
                    text.Focus();
                });
            }
            else
            {
                view.Visibility = Visibility.Visible;
                edit.Visibility = Visibility.Collapsed;

            }
        }
    }
}
