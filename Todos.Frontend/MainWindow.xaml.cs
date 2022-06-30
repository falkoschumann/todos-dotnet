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
        }

        private void DestroyTodo(object sender, RoutedEventArgs e)
        {
            var control = (Button)sender;
            OnDestroyTodoCommand(new DestroyTodoCommand((int)control.Tag));
        }

        private void ToggleTodo(object sender, RoutedEventArgs e)
        {
            var control = (CheckBox)sender;
            OnToggleTodoCommand(new ToggleTodoCommand((int)control.Tag));
        }
    }
}
